using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IFrameRepository
{
    Task<IEnumerable<Frame>> GetAll();
    Task<IEnumerable<Frame>> GetByName(string name);
    Task<Guid> AddAsync(Frame frame);
    Task<Frame?> GetByIdAsync(Guid frameId);
    Task RemoveAsync(Frame frame);
    Task UpdateAsync(Frame frame);
}
