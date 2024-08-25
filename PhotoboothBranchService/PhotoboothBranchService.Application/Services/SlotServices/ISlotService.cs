using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Slot;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.SlotServices
{
    public interface ISlotService : IServiceBase<SlotResponse,SlotFilter,PagingModel>
    {
        Task<IEnumerable<SlotResponse>> GetByBoothId(Guid boothID);
        Task<IEnumerable<SlotResponse>> GetBoothFreeSlot(Guid boothID, DateOnly date);
        Task<IEnumerable<SlotResponse>> AutoCreateSlotByBooth(AutoCreateSlotRequest request);
        Task<IEnumerable<GetBranchFreeSlotResponse>> GetBranchFreeSlot(Guid BranchID, DateOnly date);
        Task UpdateSlotPriceForBooth(Guid boothID, decimal price);
        Task UpdateSlotPrice(Guid slotID, decimal price);
    }
}
