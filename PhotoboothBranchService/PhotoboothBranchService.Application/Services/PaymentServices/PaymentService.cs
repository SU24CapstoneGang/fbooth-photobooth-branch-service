using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
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

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<Guid> CreateAsync(CreatePaymentRequest createModel)
        {
            var payment = _mapper.Map<Payment>(createModel);
            payment.PaymentStatus = PaymentStatus.Processing;
            return await _paymentRepository.AddAsync(payment);
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
            listPaymentResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listPaymentResponse;
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
