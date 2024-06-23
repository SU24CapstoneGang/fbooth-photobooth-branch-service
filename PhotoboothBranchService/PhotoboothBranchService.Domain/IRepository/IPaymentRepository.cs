using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Domain.IRepository
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        public Task<bool> IsOrderPaid(Guid SessionOrderID);
    }
}
