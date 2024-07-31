using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Application.Services.MoMoServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Application.Services.VNPayServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Net;

namespace PhotoboothBranchService.Application.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IVNPayService _vNPayService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMoMoService _moMoService;
        private readonly IEmailService _emailService;
        private readonly IRefundService _refundService;
        public TransactionService(ITransactionRepository paymentRepository, IMapper mapper,
            IPaymentMethodRepository paymentMethodRepository,
            IVNPayService vNPayService,
            IBookingRepository sessionOrderRepository,
            IMoMoService moMoService,
            IEmailService emailService,
            IRefundService refundService)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _paymentMethodRepository = paymentMethodRepository;
            _vNPayService = vNPayService;
            _bookingRepository = sessionOrderRepository;
            _moMoService = moMoService;
            _emailService = emailService;
            _refundService = refundService;
        }

        // Create
        public async Task<CreatePaymentResponse> CreateAsync(CreateTransactionRequest createModel, string ClientIpAddress)
        {
            //validate session and total price
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == createModel.SessionOrderID)).FirstOrDefault();
            if (booking == null)
            {
                throw new NotFoundException("Not found Session to proceed payment");
            }
            if (booking.IsCancelled)
            {
                throw new BadRequestException("The Order has been ended or cancelled");
            }
            //add check endtime later




            //create payment object
            var payment = _mapper.Map<Transaction>(createModel);
            payment.TransactionID = Guid.NewGuid();
            payment.TransactionStatus = TransactionStatus.Processing;
            payment.TransactionDateTime = DateTime.Now;
            switch (createModel.PayType)
            {
                case PayType.FullPay:
                    var payments = await _paymentRepository.GetAsync(i => i.BookingID == booking.BookingID && i.TransactionStatus == TransactionStatus.Success);
                    long result = (long)booking.PaymentAmount - payments.Sum(i => i.Amount);
                    if (result == 0)
                    {
                        throw new BadRequestException("Already paid all");
                    }
                    else if (result < 0)
                    {
                        throw new BadRequestException("System revice more than bill, contact manager about the error");
                    }
                    payment.Amount = result;
                    break;
                    //case PayType.Deposit:
                    //    if (sessionOrder.Status != BookingStatus.Created)
                    //    {
                    //        throw new BadRequestException("Deposit only apply on Booking");
                    //    }
                    //    payment.Amount = (long)Math.Round(sessionOrder.PaymentAmount * 0.2m);
                    break;
                default:
                    // Handle unexpected PayType
                    throw new ArgumentOutOfRangeException(nameof(createModel.PayType), createModel.PayType, "Invalid payment type");
            }

            //response to return 
            CreatePaymentResponse createPaymentResponse = new CreatePaymentResponse() { PaymentID = payment.TransactionID };

            //validate and choose payment method
            var paymentMethod = (await _paymentMethodRepository.GetAsync(i => i.PaymentMethodID == payment.PaymentMethodID)).FirstOrDefault();
            if (paymentMethod != null)
            {
                //check method status
                if (paymentMethod.Status == PaymentMethodStatus.Inactive)
                {
                    throw new BadRequestException("This method is not availble or in maintenance, please try this later");
                }
                switch (paymentMethod.PaymentMethodName)
                {
                    case "VNPay":
                        VnpayRequest vnpayRequest = new VnpayRequest
                        {
                            Amount = payment.Amount,
                            ClientIpAddress = ClientIpAddress,
                            SessionOrderID = createModel.SessionOrderID,
                            OrderInformation = createModel.Description,
                            PaymentID = payment.TransactionID,
                            PaymentDateTime = payment.TransactionDateTime
                        };
                        if (!createModel.BankCode.IsNullOrEmpty())
                        {
                            vnpayRequest.BankCode = createModel.BankCode;
                        }
                        createPaymentResponse.PaymentUlr = _vNPayService.Pay(vnpayRequest);
                        break;

                    case "MoMo":
                        MoMoRequest moMoRequest = new MoMoRequest
                        {
                            amount = payment.Amount,
                            orderId = payment.TransactionID.ToString(),
                            extraData = "",
                            orderInfo = createModel.Description,
                            requestId = Guid.NewGuid().ToString(),
                        };
                        createPaymentResponse.PaymentUlr = _moMoService.CreatePayment(moMoRequest);
                        break;

                    default:
                        throw new BadRequestException("Payment method not availbe to use, please try later");
                }
            }
            else
            {
                throw new NotFoundException("Not found Payment method as request");
            }

            //add payment to DB and return url
            await _paymentRepository.AddAsync(payment);

            return createPaymentResponse;
        }
        //handle response
        public async Task<(VnpayResponse response, string returnContent)> HandleVnpayResponse(IQueryCollection queryString)
        {
            var result = await _vNPayService.Return(queryString);
            var responseBody = result.response;
            if (responseBody.Success)
            {
                try
                {
                    await _emailService.SendBillInformation(responseBody.PaymentID);
                    await updateAfterSuccessPaymentAsync(result.paymentResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (result.response, result.returnContent);
        }
        public async Task HandleMomoResponse(IQueryCollection queryString)
        {
            var response = await _moMoService.Return(queryString);
            if (response.TransactionStatus == TransactionStatus.Success)
            {
                try
                {
                    await _emailService.SendBillInformation(response.TransactionID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                await updateAfterSuccessPaymentAsync(response);
            }
        }
        public async Task<MomoIPNResponse> HandleMomoIPN(MoMoResponse moMoResponse)
        {
            return (await _moMoService.HandlePaymentResponeIPN(moMoResponse)).iPNResponse;
        }
        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var payments = await _paymentRepository.GetAsync(p => p.TransactionID == id);
                var payment = payments.FirstOrDefault();
                if (payment != null)
                {
                    await _paymentRepository.RemoveAsync(payment);
                }
            }
            catch
            {
                throw;
            }
        }
        // Read all
        public async Task<IEnumerable<TransactionResponse>> GetAllAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments.ToList());
        }
        // Read all with paging and filter
        public async Task<IEnumerable<TransactionResponse>> GetAllPagingAsync(PaymentFilter filter, PagingModel paging)
        {
            var payments = (await _paymentRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPaymentResponse = _mapper.Map<IEnumerable<TransactionResponse>>(payments);
            return listPaymentResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }
        public async Task<IEnumerable<TransactionResponse>> GetBySessionOrderAsync(Guid sessionOrderID)
        {
            var payments = await _paymentRepository.GetAsync(i => i.BookingID == sessionOrderID);
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments.ToList());
        }
        public async Task<IEnumerable<TransactionResponse>> GetByOrderIdAsync(Guid id)
        {
            var payments = await _paymentRepository.GetAsync(p => p.BookingID == id);
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments);
        }
        // Read by ID
        public async Task<TransactionResponse> GetByIdAsync(Guid id)
        {
            var payments = await _paymentRepository.GetAsync(p => p.TransactionID == id);
            var payment = payments.FirstOrDefault();
            return _mapper.Map<TransactionResponse>(payment);
        }
        // Update
        public async Task UpdateAsync(Guid id, UpdateTransactiontRequest updateModel)
        {
            var payment = (await _paymentRepository.GetAsync(p => p.TransactionID == id)).FirstOrDefault();
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found.");
            }

            var updatedPayment = _mapper.Map(updateModel, payment);
            await _paymentRepository.UpdateAsync(updatedPayment);
        }
        private async Task updateAfterSuccessPaymentAsync(Transaction payment)
        {
            //var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.BookingID == payment.BookingID)).FirstOrDefault();
            //if (sessionOrder != null && sessionOrder.Status == BookingStatus.Created)
            //{
            //    if (payment.Amount < sessionOrder.PaymentAmount)
            //    {
            //        sessionOrder.Status = BookingStatus.Deposited;
            //    }
            //    else if (payment.Amount == sessionOrder.PaymentAmount)
            //    {
            //        sessionOrder.Status = BookingStatus.Waiting;
            //        try
            //        {
            //            if (sessionOrder.StartTime > DateTime.Now)
            //            {
            //                await _emailService.SendBookingInformation(sessionOrder.BookingID);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //    await _sessionOrderRepository.UpdateAsync(sessionOrder);
            //}
            //else if (sessionOrder != null && sessionOrder.Status == BookingStatus.Deposited)
            //{
            //    var paymentCheck = (await _paymentRepository.GetAsync(i => i.BookingID == sessionOrder.BookingID && i.TransactionStatus == PaymentStatus.Success)).ToList();
            //    if (paymentCheck.Sum(i => i.Amount) == sessionOrder.PaymentAmount)
            //    {
            //        sessionOrder.Status = BookingStatus.Waiting;
            //        try
            //        {
            //            if (sessionOrder.StartTime > DateTime.Now)
            //            {
            //                await _emailService.SendBookingInformation(sessionOrder.BookingID);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //        await _sessionOrderRepository.UpdateAsync(sessionOrder);
            //    }
            //}
        }
    }
}
