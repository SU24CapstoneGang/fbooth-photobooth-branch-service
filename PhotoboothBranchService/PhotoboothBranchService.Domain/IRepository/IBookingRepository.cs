using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task updateTotalPrice(Guid SessionOrderID);

    Task<Booking> GetBookingByValidateCodeAndBoothIdAsync(long validateCode, Guid boothId);

}
