using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            PaymentMethod paymentMethod = _mapper.Map<PaymentMethod>(createModel);
            return await _paymentMethodRepository.AddAsync(paymentMethod);
        }

        public async Task DeleteAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id);
            var paymentMethodToDelete = paymentMethod.FirstOrDefault();
            if (paymentMethodToDelete != null)
            {
                await _paymentMethodRepository.RemoveAsync(paymentMethodToDelete);
            }
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetAllAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods.ToList());
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetAllPagingAsync(PaymentMethodFilter filter, PagingModel paging)
        {
            var paymentMethods = (await _paymentMethodRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var paymentMethodsResponse = _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods);
            paymentMethodsResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return paymentMethodsResponse;
        }

        public async Task<PaymentMethodResponse> GetByIdAsync(Guid id)
        {
            var paymentMethods = await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id);
            return _mapper.Map<PaymentMethodResponse>(paymentMethods);
        }

        public async Task<IEnumerable<PaymentMethodResponse>> GetByName(string name)
        {
            var paymentMethods = await _paymentMethodRepository.GetAsync(i => i.PaymentMethodName.Equals(name));
            return _mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods.ToList());
        }

        public async Task UpdateAsync(Guid id, UpdatePaymentMethodRequest updateModel)
        {
            var paymentMethod = (await _paymentMethodRepository.GetAsync(p => p.PaymentMethodID == id)).FirstOrDefault();
            if (paymentMethod == null)
            {
                throw new KeyNotFoundException("Payment method not found.");
            }

            var updatedPaymentMethod = _mapper.Map(updateModel, paymentMethod);
            await _paymentMethodRepository.UpdateAsync(updatedPaymentMethod);
        }
    }
}
