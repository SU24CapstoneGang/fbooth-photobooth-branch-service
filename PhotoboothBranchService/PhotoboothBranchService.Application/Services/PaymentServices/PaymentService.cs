using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using PhotoboothBranchService.Application.Services.PaymentServices.MoMoServices;
using PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IVNPayService _vNPayService;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        private readonly IMoMoService _moMoService;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper,
            IPaymentMethodRepository paymentMethodRepository,
            IVNPayService vNPayService,
            ISessionOrderRepository sessionOrderRepository,
            IMoMoService moMoService)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _paymentMethodRepository = paymentMethodRepository;
            _vNPayService = vNPayService;
            _sessionOrderRepository = sessionOrderRepository;
            _moMoService = moMoService;
        }

        // Create
        public async Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createModel)
        {
            //validate session and total price
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.SessionOrderID == createModel.SessionOrderID)).FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Session to proceed payment");
            }

            //create payment object
            var payment = _mapper.Map<Payment>(createModel);
            payment.PaymentID = Guid.NewGuid();
            payment.PaymentStatus = PaymentStatus.Processing;
            payment.PaymentDateTime = DateTime.Now;
            switch (createModel.PayType)
            {
                case PayType.FullPay:
                    var payments = await _paymentRepository.GetAsync(i => i.SessionOrderID == sessionOrder.SessionOrderID && i.PaymentStatus == PaymentStatus.Success);
                    long result = (long)sessionOrder.TotalPrice - payments.Sum(i => i.Amount);
                    if (result == 0)
                    {
                        throw new Exception("Already paid all");
                    }
                    else if (result < 0)
                    {
                        throw new Exception("System revice more than bill, contact manager about the error");
                    }
                    payment.Amount = result;
                    break;
                case PayType.Deposit:
                    if (sessionOrder.Status != SessionOrderStatus.Created)
                    {
                        throw new Exception("Deposit only apply on Booking");
                    }
                    payment.Amount = (long)Math.Round(sessionOrder.TotalPrice * 0.2m);
                    break;
                default:
                    // Handle unexpected PayType
                    throw new ArgumentOutOfRangeException(nameof(createModel.PayType), createModel.PayType, "Invalid payment type");
            }

            //response to return 
            CreatePaymentResponse createPaymentResponse = new CreatePaymentResponse() { PaymentID = payment.PaymentID };

            //validate and choose payment method
            var paymentMethod = (await _paymentMethodRepository.GetAsync(i => i.PaymentMethodID == createModel.PaymentMethodID)).FirstOrDefault();
            if (paymentMethod != null)
            {
                //check method status
                if (paymentMethod.Status == PaymentMethodStatus.Inactive)
                {
                    throw new Exception("This method is not availble or in maintenance, please try this later");
                }
                switch (paymentMethod.PaymentMethodName)
                {
                    case "VNPay":
                        VnpayRequest vnpayRequest = new VnpayRequest
                        {
                            Amount = payment.Amount,
                            ClientIpAddress = createModel.ClientIpAddress,
                            SessionOrderID = createModel.SessionOrderID,
                            OrderInformation = createModel.Description,
                            PaymentID = payment.PaymentID,
                            PaymentDateTime = payment.PaymentDateTime
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
                            orderId = payment.PaymentID.ToString(),
                            extraData = "",
                            orderInfo = createModel.Description,
                            requestId = Guid.NewGuid().ToString(),
                        };
                        createPaymentResponse.PaymentUlr = _moMoService.CreatePayment(moMoRequest);
                        break;

                    default:
                        throw new Exception("Payment method not availbe to use, please try later");
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
            return _mapper.Map<IEnumerable<PaymentResponse>>(payments.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<PaymentResponse>> GetAllPagingAsync(PaymentFilter filter, PagingModel paging)
        {
            var payments = (await _paymentRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPaymentResponse = _mapper.Map<IEnumerable<PaymentResponse>>(payments);
            return listPaymentResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
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
    }
}
