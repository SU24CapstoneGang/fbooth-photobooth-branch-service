using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> GetAll();
    Task<Guid> AddAsync(Session session);
    Task<Session?> GetByIdAsync(Guid sessionId);
    Task RemoveAsync(Session session);
    Task UpdateAsync(Session session);
}
