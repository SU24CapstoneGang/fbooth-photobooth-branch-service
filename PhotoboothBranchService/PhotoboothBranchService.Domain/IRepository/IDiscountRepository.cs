using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IDiscountRepository : IRepositoryBase<Discount>
{
    public Task<IEnumerable<Discount>> GetByCode(string code);
}
