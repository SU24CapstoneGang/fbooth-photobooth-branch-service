using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IDiscountRepository
{
    Task<IEnumerable<Discount>> GetAll();
    Task<IEnumerable<Discount>> GetByCode(string code);
    Task<Guid> AddAsync(Discount discount);
    Task<Discount?> GetByIdAsync(Guid discountId);
    Task RemoveAsync(Discount discount);
    Task UpdateAsync(Discount discount);
}
