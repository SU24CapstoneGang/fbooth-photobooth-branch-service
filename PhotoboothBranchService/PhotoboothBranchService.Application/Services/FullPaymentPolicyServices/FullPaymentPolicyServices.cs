using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.FullPaymentPolicy;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FullPaymentPolicyServices
{
    public class FullPaymentPolicyServices : IFullPaymentPolicyServices
    {
        private readonly IFullPaymentPolicyRepository _repository;
        private readonly IMapper _mapper;

        public FullPaymentPolicyServices(IFullPaymentPolicyRepository fullPaymentPolicyRepository, IMapper mapper)
        {
            _repository = fullPaymentPolicyRepository;
            _mapper = mapper;
        }
        public async Task<FullPaymentPolicyResponse> CreatePolicy(CreatePolicyRequestModel request)
        {
            // Kiểm tra các giá trị đầu vào
            if (string.IsNullOrEmpty(request.PolicyName))
            {
                throw new ArgumentException("Policy name is required.");
            }

            if (request.RefundDaysBefore < 0)
            {
                throw new ArgumentException("Refund days before cannot be negative.");
            }
            var timeNow = DateTimeHelper.GetVietnamTimeNow();
            // Kiểm tra ngày bắt đầu và ngày kết thúc
            if (request.StartDate.HasValue && request.StartDate.Value.ToDateTime(TimeOnly.MinValue) < timeNow)
            {
                throw new ArgumentException("Start date cannot be in the past.");
            }

            if (request.EndDate.HasValue && request.EndDate.Value.ToDateTime(TimeOnly.MinValue) < timeNow)
            {
                throw new ArgumentException("End date cannot be in the past.");
            }

            // Kiểm tra trùng lặp chính sách dựa trên thời gian hiệu lực
            var existingPolicies = await _repository.GetAsync(
             p => (!request.StartDate.HasValue || !p.EndDate.HasValue || request.StartDate <= p.EndDate) &&
                  (!request.EndDate.HasValue || !p.StartDate.HasValue || request.EndDate >= p.StartDate));


            if (existingPolicies.Any())
            {
                throw new InvalidOperationException("A policy with overlapping effective dates already exists.");
            }

            // Kiểm tra và đặt trạng thái của chính sách
            bool isActive = !request.StartDate.HasValue || request.StartDate.Value.ToDateTime(TimeOnly.MinValue) <= timeNow;

            var policy = new FullPaymentPolicy
            {
                FullPaymentPolicyID = Guid.NewGuid(),
                PolicyName = request.PolicyName,
                PolicyDescription = request.PolicyDescription,
                RefundDaysBefore = request.RefundDaysBefore,
                CheckInTimeLimit = request.CheckInTimeLimit,
                IsActive = isActive,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = timeNow,
                IsDefaultPolicy = !request.StartDate.HasValue,
            };

            await _repository.AddAsync(policy);

            return _mapper.Map<FullPaymentPolicyResponse>(policy);
        }

        public async Task DeleteAsync(Guid id)
        {
            var policy = (await _repository.GetAsync(p => p.FullPaymentPolicyID == id)).FirstOrDefault();
            if (policy == null)
            {
                throw new KeyNotFoundException("Policy not found.");
            }
            await _repository.RemoveAsync(policy);
        }

        public async Task<IEnumerable<FullPaymentPolicyResponse>> GetAllAsync()
        {
            var policy = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FullPaymentPolicyResponse>>(policy.ToList());
        }

        public async Task<IEnumerable<FullPaymentPolicyResponse>> GetAllPagingAsync(FullPaymentPolicyFilter filter, PagingModel paging)
        {
            var policy = (await _repository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPolicyresponse = _mapper.Map<IEnumerable<FullPaymentPolicyResponse>>(policy);
            return listPolicyresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        public async Task<FullPaymentPolicyResponse> GetByIdAsync(Guid id)
        {
            var policy = (await _repository.GetAsync(p => p.FullPaymentPolicyID == id)).FirstOrDefault();
            return _mapper.Map<FullPaymentPolicyResponse>(policy);
        }

        public async Task UpdatePolicyAsync(Guid id, UpdatePolicyRequestModel policyRequest)
        {
            var policy = (await _repository.GetAsync(p => p.FullPaymentPolicyID == id)).FirstOrDefault();
            if (policy == null)
            {
                throw new KeyNotFoundException("Policy not found.");
            }

            // Kiểm tra các giá trị đầu vào
            //if (string.IsNullOrEmpty(policyRequest.PolicyName))
            //{
            //    throw new ArgumentException("Policy name is required.");
            //}

            if (policyRequest.RefundDaysBefore < 0)
            {
                throw new ArgumentException("Refund days before cannot be negative.");
            }

            // Kiểm tra ngày bắt đầu và ngày kết thúc
            if (policyRequest.StartDate.HasValue && policyRequest.StartDate.Value.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
            {
                throw new ArgumentException("Start date cannot be in the past.");
            }

            if (policyRequest.EndDate.HasValue && policyRequest.EndDate.Value.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
            {
                throw new ArgumentException("End date cannot be in the past.");
            }

            //// Kiểm tra trùng lặp chính sách dựa trên thời gian hiệu lực
            //var existingPolicies = await _repository.GetAsync(
            //    p => p.FullPaymentPolicyID != id &&
            //         (policyRequest.StartDate == null || p.EndDate == null || policyRequest.StartDate <= p.EndDate) &&
            //         (policyRequest.EndDate == null || p.StartDate == null || policyRequest.EndDate >= p.StartDate));

            //if (existingPolicies.Any())
            //{
            //    throw new InvalidOperationException("A policy with overlapping effective dates already exists.");
            //}

            var updatePolicy = _mapper.Map(policyRequest, policy);
            await _repository.UpdateAsync(updatePolicy);
        }
    }
}
