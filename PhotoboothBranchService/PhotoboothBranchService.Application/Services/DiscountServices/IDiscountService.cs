using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Discount;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Discount;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.DiscountServices;

public interface IDiscountService : IService<Discountresponse,CreateDiscountRequest,UpdateDiscountRequest,DiscountFilter,PagingModel>
{
    Task<IEnumerable<Discountresponse>> SearchByCode(string code);
    Task<Discountresponse> GetByCode(string code);
}
