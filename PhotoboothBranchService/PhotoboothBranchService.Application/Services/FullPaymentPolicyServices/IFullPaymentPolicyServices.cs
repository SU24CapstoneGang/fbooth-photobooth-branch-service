using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FullPaymentPolicy;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FullPaymentPolicyServices
{
    public interface IFullPaymentPolicyServices : IServiceBase<FullPaymentPolicyResponse, FullPaymentPolicyFilter, PagingModel>
    {
        Task<FullPaymentPolicyResponse> CreatePolicy(CreatePolicyRequestModel layoutRequest);
        Task DeleteAsync(Guid id);
        Task UpdatePolicyAsync(Guid id, UpdatePolicyRequestModel policyRequest );
    }
}
