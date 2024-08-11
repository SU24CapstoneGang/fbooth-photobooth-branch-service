using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.Slot;
using PhotoboothBranchService.Application.Services.SlotServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class SlotController : ControllerBaseApi
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetAllServiceTypes()
        {
            var services = await _slotService.GetAllAsync();
            return Ok(services.ToList().OrderBy(i => i.SlotStartTime));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> AutoCreate(AutoCreateSlotRequest request)
        {
            var slots = await _slotService.AutoCreateSlotByBooth(request);
            return Ok(slots);
        }

        [HttpGet("booth")]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetBoothFreeSLot([FromQuery]GetBoothFreeSlotRequest request)
        {
            var slots = await _slotService.GetBoothFreeSlot(request.BoothID, request.date, request.startTime, request.endTime);
            return Ok(slots);
        }
        [HttpGet("branch")]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetBranchFreeSLot([FromQuery] GetBranchFreeSlotRequest request)
        {
            var slots = await _slotService.GetBranchFreeSlot(request.BranchID, request.date, request.startTime, request.endTime);
            return Ok(slots);
        }
    }
}
