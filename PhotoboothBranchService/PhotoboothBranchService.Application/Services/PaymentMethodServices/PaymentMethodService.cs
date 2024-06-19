using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PaymentMethodServices
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreatePaymentMethodRequest createModel)
        {
            try
            {
                var isPaymentExist = (await _paymentMethodRepository.GetAsync(p => p.PaymentMethodName.Equals(createModel.PaymentMethodName))).FirstOrDefault();
                if (isPaymentExist != null) throw new BadRequestException("Payment method is already existed");

                PaymentMethod paymentMethod = _mapper.Map<PaymentMethod>(createModel);
                return await _paymentMethodRepository.AddAsync(paymentMethod);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create Payment Method: " + ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id);
                var paymentMethodToDelete = paymentMethod.FirstOrDefault();
                if (paymentMethodToDelete == null) throw new NotFoundException("Payment method", id, "Payment method ID not found");

                await _paymentMethodRepository.RemoveAsync(paymentMethodToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting Payment Method: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetAllAsync()
        {
            try
            {
                var paymentMethods = await _paymentMethodRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting Payment Method: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetAllPagingAsync(PaymentMethodFilter filter, PagingModel paging)
        {
            try
            {
                var paymentMethods = (await _paymentMethodRepository.GetAllAsync()).ToList().AutoFilter(filter);
                var paymentMethodsResponse = _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods);
                paymentMethodsResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
                return paymentMethodsResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting Payment Method: " + ex.Message);
            }
        }

        public async Task<PaymentMethodResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var paymentMethods = (await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id)).FirstOrDefault();
                if (paymentMethods == null) throw new NotFoundException("Payment method", id, "Payment method ID not found");
                return _mapper.Map<PaymentMethodResponse>(paymentMethods);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create Payment Method: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetByName(string name)
        {
            try
            {
                var paymentMethods = await _paymentMethodRepository.GetAsync(i => i.PaymentMethodName.Equals(name));
                if (paymentMethods == null) throw new NotFoundException("Payment method", name, "Payment method name not found");
                return _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create Payment Method: " + ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, UpdatePaymentMethodRequest updateModel)
        {
            try
            {
                var paymentMethod = (await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id)).FirstOrDefault();
                if (paymentMethod == null)
                    throw new NotFoundException("Payment method", id, "Payment method id not found");

                var updatedPaymentMethod = _mapper.Map(updateModel, paymentMethod);
                await _paymentMethodRepository.UpdateAsync(updatedPaymentMethod);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create Payment Method: " + ex.Message);
            }
        }
    }
}
