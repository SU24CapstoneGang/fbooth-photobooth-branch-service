using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Discount;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.DiscountServices;

public interface IDiscountService : IService<Discountresponse, CreateDiscountRequest, UpdateDiscountRequest, DiscountFilter, PagingModel>
{
    Task<IEnumerable<Discountresponse>> SearchByCode(string code);
    Task<Discountresponse> GetByCode(string code);
}
