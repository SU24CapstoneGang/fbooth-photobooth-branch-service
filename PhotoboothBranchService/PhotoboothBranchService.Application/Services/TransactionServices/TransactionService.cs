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
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IVNPayService _vNPayService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMoMoService _moMoService;
        private readonly IEmailService _emailService;
        private readonly IRefundService _refundService;
        private readonly IAccountRepository _accountRepository;
        public TransactionService(ITransactionRepository paymentRepository, IMapper mapper,
            IPaymentMethodRepository paymentMethodRepository,
            IVNPayService vNPayService,
            IBookingRepository sessionOrderRepository,
            IMoMoService moMoService,
            IEmailService emailService,
            IRefundService refundService,
            IAccountRepository accountRepository)
        {
            _transactionRepository = paymentRepository;
            _mapper = mapper;
            _paymentMethodRepository = paymentMethodRepository;
            _vNPayService = vNPayService;
            _bookingRepository = sessionOrderRepository;
            _moMoService = moMoService;
            _emailService = emailService;
            _refundService = refundService;
            _accountRepository = accountRepository;
        }

        // Create
        public async Task<CreateTransactionResponse> CreateAsync(CreateTransactionRequest createModel, string ClientIpAddress, string? email)
        {
            //validate session and total price
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == createModel.BookingID, i => i.Booth)).FirstOrDefault();
            if (booking == null)
            {
                throw new NotFoundException("Not found Session to proceed payment");
            }
            if (booking.IsCancelled)
            {
                throw new BadRequestException("The Order has been ended or cancelled");
            }
            //add check endtime later
            if (booking.PaymentStatus == PaymentStatus.Paid && booking.Status == BookingStatus.PendingChecking)
            {
                throw new BadRequestException("Booking already paid.");
            }

            var account = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
            if (account == null)
            {
                throw new BadRequestException("Account not found.");
            }
            //create payment object
            var transaction = _mapper.Map<Transaction>(createModel);
            transaction.TransactionID = Guid.NewGuid();
            transaction.TransactionStatus = TransactionStatus.Processing;
            transaction.TransactionDateTime = DateTimeHelper.GetVietnamTimeNow();

            var payments = await _transactionRepository.GetAsync(i => i.BookingID == booking.BookingID && i.TransactionStatus == TransactionStatus.Success);
            long result = (long)booking.PaymentAmount - payments.Sum(i => i.Amount);
            if (result == 0)
            {
                throw new BadRequestException("Booking already paid.");
            }
            else if (result < 0)
            {
                throw new BadRequestException("System revice more than bill, contact manager about the error");
            }
            transaction.Amount = result;

            //response to return 
            CreateTransactionResponse createPaymentResponse = new CreateTransactionResponse() { TransactionID = transaction.TransactionID };

            //validate and choose payment method
            var paymentMethod = (await _paymentMethodRepository.GetAsync(i => i.PaymentMethodID == transaction.PaymentMethodID)).FirstOrDefault();
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
                        if (account.Role == AccountRole.Customer)
                        {
                            if (account.AccountID != booking.CustomerID)
                            {
                                throw new BadRequestException("Booking is requested transaction not from it's Customer ");
                            }
                        }
                        VnpayRequest vnpayRequest = new VnpayRequest
                        {
                            Amount = transaction.Amount,
                            ClientIpAddress = ClientIpAddress,
                            BookingID = createModel.BookingID,
                            OrderInformation = createModel.Description,
                            PaymentID = transaction.TransactionID,
                            PaymentDateTime = transaction.TransactionDateTime,
                            ReturnUrl = createModel.ReturnUrl,
                        };
                        if (!createModel.BankCode.IsNullOrEmpty())
                        {
                            vnpayRequest.BankCode = createModel.BankCode;
                        }
                        createPaymentResponse.TransactionUlr = _vNPayService.Pay(vnpayRequest);
                        break;

                    case "MoMo":
                        if (account.Role == AccountRole.Customer)
                        {
                            if (account.AccountID != booking.CustomerID)
                            {
                                throw new BadRequestException("Booking is requested transaction not from it's Customer ");
                            }
                        }
                        MoMoRequest moMoRequest = new MoMoRequest
                        {
                            amount = transaction.Amount,
                            orderId = transaction.TransactionID.ToString(),
                            extraData = "",
                            orderInfo = createModel.Description,
                            requestId = Guid.NewGuid().ToString(),
                            ReturnUrl = createModel.ReturnUrl,
                        };
                        createPaymentResponse.TransactionUlr = _moMoService.CreatePayment(moMoRequest);
                        break;
                    case "Cash":
                        if (account.Role == AccountRole.Customer)
                        {
                            throw new ForbiddenAccessException("This method can not be use by customer account");
                        }
                        if (account.Role != AccountRole.Admin)
                        {
                            if (account.BranchID.Value != booking.Booth.BranchID)
                            {
                                throw new ForbiddenAccessException("Booking and Staff/Manager are not from same branch to create this transaction");
                            }
                        }

                        createPaymentResponse.TransactionID = transaction.TransactionID;
                        createPaymentResponse.TransactionUlr = "";
                        transaction.GatewayTransactionID = account.AccountID.ToString();
                        transaction.TransactionStatus = TransactionStatus.Success;
                        await _transactionRepository.AddAsync(transaction);
                        await this.updateAfterSuccessPaymentAsync(transaction);
                        return createPaymentResponse;
                    default:
                        throw new BadRequestException("Payment method not availbe to use, please try later");
                }
            }
            else
            {
                throw new NotFoundException("Not found Payment method as request");
            }

            //add payment to DB and return url
            await _transactionRepository.AddAsync(transaction);

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
                await updateAfterSuccessPaymentAsync(response);
            }
        }
        public async Task<MomoIPNResponse> HandleMomoIPN(MoMoResponse moMoResponse)
        {
            var response = (await _moMoService.HandlePaymentResponeIPN(moMoResponse));
            if (response.transaction.TransactionStatus == TransactionStatus.Success)
            {
                await updateAfterSuccessPaymentAsync(response.transaction);
            }
            return response.iPNResponse;
        }
        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var payments = await _transactionRepository.GetAsync(p => p.TransactionID == id);
                var payment = payments.FirstOrDefault();
                if (payment != null)
                {
                    await _transactionRepository.RemoveAsync(payment);
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
            var payments = await _transactionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments.ToList());
        }
        // Read all with paging and filter
        public async Task<IEnumerable<TransactionResponse>> GetAllPagingAsync(PaymentFilter filter, PagingModel paging)
        {
            var payments = (await _transactionRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPaymentResponse = _mapper.Map<IEnumerable<TransactionResponse>>(payments);
            return listPaymentResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }
        public async Task<IEnumerable<TransactionResponse>> GetBySessionOrderAsync(Guid sessionOrderID)
        {
            var payments = await _transactionRepository.GetAsync(i => i.BookingID == sessionOrderID);
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments.ToList());
        }
        public async Task<IEnumerable<TransactionResponse>> GetByOrderIdAsync(Guid id)
        {
            var payments = await _transactionRepository.GetAsync(p => p.BookingID == id);
            return _mapper.Map<IEnumerable<TransactionResponse>>(payments);
        }
        // Read by ID
        public async Task<TransactionResponse> GetByIdAsync(Guid id)
        {
            var payments = await _transactionRepository.GetAsync(p => p.TransactionID == id);
            var payment = payments.FirstOrDefault();
            return _mapper.Map<TransactionResponse>(payment);
        }
        // Update
        public async Task UpdateAsync(Guid id, UpdateTransactiontRequest updateModel)
        {
            var payment = (await _transactionRepository.GetAsync(p => p.TransactionID == id)).FirstOrDefault();
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found.");
            }

            var updatedPayment = _mapper.Map(updateModel, payment);
            await _transactionRepository.UpdateAsync(updatedPayment);
        }
        private async Task updateAfterSuccessPaymentAsync(Transaction trans)
        {
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == trans.BookingID)).FirstOrDefault();
            if (booking == null)
            {
                //do refund trans
                throw new Exception("Not found Booking");
            }
            if (booking.Status == BookingStatus.PendingPayment && booking.PaymentStatus == PaymentStatus.Processing)
            {
                if (trans.Amount == booking.PaymentAmount)
                {
                    booking.Status = BookingStatus.PendingChecking;
                    booking.PaymentStatus = PaymentStatus.Paid;
                    try
                    {
                        if (booking.StartTime > DateTimeHelper.GetVietnamTimeNow())
                        {
                            await _emailService.SendBookingInformation(booking.BookingID, trans.TransactionID);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                await _bookingRepository.UpdateAsync(booking);
            }
            else
            {
                //do refund
                await _refundService.RefundByTransID(trans.TransactionID, true, null, null);
            }
        }
    }
}
