using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.DiscountServices;

public interface IDiscountService : IService<DiscountDTO>
{
    Task<IEnumerable<DiscountDTO>> GetByCode(string code);
}
