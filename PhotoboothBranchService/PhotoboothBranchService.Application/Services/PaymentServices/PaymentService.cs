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
using System.Formats.Asn1;
using System.Net;

namespace PhotoboothBranchService.Application.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IVNPayService _vNPayService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMoMoService _moMoService;
        private readonly IEmailService _emailService;
        private readonly IRefundService _refundService;
        private readonly IAccountRepository _accountRepository;
        private readonly IBoothRepository _boothRepository;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper,
            IPaymentMethodRepository paymentMethodRepository,
            IVNPayService vNPayService,
            IBookingRepository sessionOrderRepository,
            IMoMoService moMoService,
            IEmailService emailService,
            IRefundService refundService,
            IAccountRepository accountRepository, IBoothRepository boothRepository)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _paymentMethodRepository = paymentMethodRepository;
            _vNPayService = vNPayService;
            _bookingRepository = sessionOrderRepository;
            _moMoService = moMoService;
            _emailService = emailService;
            _refundService = refundService;
            _accountRepository = accountRepository;
            _boothRepository = boothRepository;
        }

        // Create
        public async Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createModel, string ClientIpAddress, string? email)
        {
            //validate session and total price
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == createModel.BookingID, i => i.Booth)).FirstOrDefault();
            if (booking == null)
            {
                throw new NotFoundException("Not found Session to proceed payment");
            }
            if (booking.BookingStatus == BookingStatus.Canceled)
            {
                throw new BadRequestException("The Order has been ended or cancelled");
            }
            //add check endtime later
            if (booking.PaymentStatus == PaymentStatus.Paid && booking.BookingStatus == BookingStatus.PendingChecking)
            {
                throw new BadRequestException("Booking already paid.");
            }

            var account = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
            if (account == null)
            {
                throw new BadRequestException("Account not found.");
            }
            //create payment object
            var payment = _mapper.Map<Payment>(createModel);
            payment.PaymentID = Guid.NewGuid();
            payment.Status = TransactionStatus.Processing;
            payment.PaymentDateTime = DateTimeHelper.GetVietnamTimeNow();

            long result = (long)booking.TotalPrice - (long)booking.PaidAmount;
            if (result == 0)
            {
                throw new BadRequestException("Booking already paid.");
            }
            else if (result < 0)
            {
                throw new BadRequestException("System revice more than bill, contact our staff about the error");
            }
            payment.Amount = result;

            //response to return 
            CreatePaymentResponse createPaymentResponse = new CreatePaymentResponse() { PaymentID = payment.PaymentID };

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
                        if (account.Role == AccountRole.Customer)
                        {
                            if (account.AccountID != booking.CustomerID)
                            {
                                throw new BadRequestException("Booking is requested payment not from it's Customer ");
                            }
                        }
                        VnpayRequest vnpayRequest = new VnpayRequest
                        {
                            Amount = payment.Amount,
                            ClientIpAddress = ClientIpAddress,
                            BookingID = createModel.BookingID,
                            OrderInformation = createModel.Description,
                            PaymentID = payment.PaymentID,
                            PaymentDateTime = payment.PaymentDateTime,
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
                                throw new BadRequestException("Booking is requested payment not from it's Customer ");
                            }
                        }
                        MoMoRequest moMoRequest = new MoMoRequest
                        {
                            amount = payment.Amount,
                            orderId = payment.PaymentID.ToString(),
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
                            if (!account.BranchID.HasValue)
                            {
                                throw new ForbiddenAccessException("Staff are not from any branch to create this transaction");
                            }
                            if (account.BranchID.Value != booking.Booth.BranchID)
                            {
                                throw new ForbiddenAccessException("Booking and Staff are not from same branch to create this transaction");
                            }
                        }

                        createPaymentResponse.PaymentID = payment.PaymentID;
                        createPaymentResponse.TransactionUlr = createModel.ReturnUrl;
                        payment.TransactionID = account.AccountID.ToString();
                        payment.Status = TransactionStatus.Success;
                        await _paymentRepository.AddAsync(payment);
                        await this.UpdateAfterSuccessPaymentAsync(payment);
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

                    await UpdateAfterSuccessPaymentAsync(result.paymentResult);
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
            if (response.Status == TransactionStatus.Success)
            {
                await UpdateAfterSuccessPaymentAsync(response);
            }
        }
        public async Task<MomoIPNResponse> HandleMomoIPN(MoMoResponse moMoResponse)
        {
            var response = (await _moMoService.HandlePaymentResponeIPN(moMoResponse));
            if (response.transaction.Status == TransactionStatus.Success)
            {
                await UpdateAfterSuccessPaymentAsync(response.transaction);
            }
            return response.iPNResponse;
        }
        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var payments = await _paymentRepository.GetAsync(p => p.PaymentID == id);
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
        public async Task<IEnumerable<PaymentResponse>> GetAllAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentResponse>>(payments.ToList().OrderByDescending(i => i.PaymentDateTime));
        }
        // Read all with paging and filter
        public async Task<IEnumerable<PaymentResponse>> GetAllPagingAsync(PaymentFilter filter, PagingModel paging)
        {
            var payments = (await _paymentRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPaymentResponse = _mapper.Map<IEnumerable<PaymentResponse>>(payments);
            return listPaymentResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.PaymentDateTime);
        }
        public async Task<IEnumerable<PaymentResponse>> GetByBookingAsync(Guid bookingID)
        {
            var payments = await _paymentRepository.GetAsync(i => i.BookingID == bookingID);
            return _mapper.Map<IEnumerable<PaymentResponse>>(payments.ToList());
        }
        public async Task<IEnumerable<PaymentResponse>> GetByOrderIdAsync(Guid id)
        {
            var payments = await _paymentRepository.GetAsync(p => p.BookingID == id);
            return _mapper.Map<IEnumerable<PaymentResponse>>(payments);
        }
        public async Task<IEnumerable<PaymentResponse>> GetCustomerTransaction(string? email)
        {
            if (email.IsNullOrEmpty())
            {
                throw new BadRequestException("");
            }
            var acc = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).SingleOrDefault();
            if (acc == null) 
            {
                throw new NotFoundException("Account not found");
            }
            if (acc.Role != AccountRole.Customer)
            {
                throw new ForbiddenAccessException("Account is not Customer");
            }
            var bookings = (await _bookingRepository.GetAsync(i => i.CustomerID == acc.AccountID)).Select(i => i.BookingID).ToList();
            if (!bookings.Any()) 
            {
                return new List<PaymentResponse>();
            }
            var trans = await _paymentRepository.GetAsync(i => bookings.Contains(i.BookingID));
            if (trans == null)
            {
                return new List<PaymentResponse>();
            } else
            {
                return _mapper.Map<IEnumerable<PaymentResponse>>(trans.ToList());
            }
        }
        public async Task<IEnumerable<PaymentResponse>> StaffGetBranchTransaction(string? email)
        {
            if (email.IsNullOrEmpty())
            {
                throw new BadRequestException("");
            }
            var acc = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).SingleOrDefault();
            if (acc == null)
            {
                throw new NotFoundException("Account not found");
            }
            if (acc.Role != AccountRole.Staff)
            {
                throw new ForbiddenAccessException("Account is not staff");
            }
            if (acc.BranchID.HasValue)
            {
                return await this.GetBranchTransaction(acc.BranchID.Value);
            } else
            {
                throw new BadRequestException("Staff has not been assigned to any brnach");
            }
        }
        public async Task<IEnumerable<PaymentResponse>> GetBranchTransaction(Guid branchID)
        {
            var booths = (await _boothRepository.GetAsync(i => i.BranchID == branchID)).Select(i => i.BoothID).ToList();
            var bookings = (await _bookingRepository.GetAsync(i => booths.Contains(i.BoothID))).Select(i => i.BookingID).ToList();
            if (!bookings.Any())
            {
                return new List<PaymentResponse>();
            }
            var trans = await _paymentRepository.GetAsync(i => bookings.Contains(i.BookingID));
            if (trans == null)
            {
                return new List<PaymentResponse>();
            }
            else
            {
                return _mapper.Map<IEnumerable<PaymentResponse>>(trans.ToList());
            }
        }
        // Read by ID
        public async Task<PaymentResponse> GetByIdAsync(Guid id)
        {
            var payments = await _paymentRepository.GetAsync(p => p.PaymentID == id);
            var payment = payments.FirstOrDefault();
            return _mapper.Map<PaymentResponse>(payment);
        }
        // Update
        public async Task UpdateAsync(Guid id, UpdatePaymentRequest updateModel)
        {
            var payment = (await _paymentRepository.GetAsync(p => p.PaymentID == id)).FirstOrDefault();
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found.");
            }

            var updatedPayment = _mapper.Map(updateModel, payment);
            await _paymentRepository.UpdateAsync(updatedPayment);
        }
        private async Task UpdateAfterSuccessPaymentAsync(Payment payment)
        {
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == payment.BookingID)).FirstOrDefault();
            if (booking == null)
            {
                //do refund trans
                throw new Exception("Not found Booking");
            }
            if (payment.Amount + booking.PaidAmount == booking.TotalPrice)
            {
                if (booking.BookingStatus == BookingStatus.PendingPayment)
                {
                    booking.BookingStatus = BookingStatus.PendingChecking;
                    try
                    {
                        if (booking.StartTime > DateTimeHelper.GetVietnamTimeNow())
                        {
                            await _emailService.SendBookingInformation(booking.BookingID, payment.PaymentID);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                booking.PaymentStatus = PaymentStatus.Paid;
                booking.PaidAmount += payment.Amount;
                await _bookingRepository.UpdateAsync(booking);
            }
            else
            {
                //do refund
                payment.Status = TransactionStatus.Redundant;
                await _paymentRepository.UpdateAsync(payment);
                await _refundService.RefundByTransID(payment.PaymentID, true, null, null, false);
            }
        }
    }
}
