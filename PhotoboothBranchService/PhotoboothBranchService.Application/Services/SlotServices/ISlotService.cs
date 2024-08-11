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
        Task<IEnumerable<SlotResponse>> GetBoothFreeSlot(Guid boothID, DateOnly date, TimeSpan? startTime, TimeSpan? endTime);
        Task<IEnumerable<SlotResponse>> AutoCreateSlotByBooth(AutoCreateSlotRequest request);
        Task<IEnumerable<GetBranchFreeSlotResponse>> GetBranchFreeSlot(Guid BranchID, DateOnly date, TimeSpan? startTime, TimeSpan? endTime);
    }
}
