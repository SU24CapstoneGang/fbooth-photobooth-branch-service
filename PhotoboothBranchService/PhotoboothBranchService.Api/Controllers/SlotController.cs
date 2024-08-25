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

        [HttpGet("{boothID}")]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetByBoothId(Guid boothID)
        {
            var slots = await _slotService.GetByBoothId(boothID);
            return Ok(slots);
        }

        [HttpGet("booth")]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetBoothFreeSLot([FromQuery]GetBoothFreeSlotRequest request)
        {
            var slots = await _slotService.GetBoothFreeSlot(request.BoothID, request.date);
            return Ok(slots);
        }
        [HttpGet("branch")]
        public async Task<ActionResult<IEnumerable<SlotResponse>>> GetBranchFreeSLot([FromQuery] GetBranchFreeSlotRequest request)
        {
            var slots = await _slotService.GetBranchFreeSlot(request.BranchID, request.date);
            return Ok(slots);
        }

        [HttpPut("slot/{slotID}")]
        public async Task<IActionResult> UpdateSlotPrice(Guid slotID, [FromBody] decimal price)
        {
            await _slotService.UpdateSlotPrice(slotID, price);
            return Ok();
        }

        [HttpPut("booth/{boothID}")]
        public async Task<IActionResult> UpdateSlotPriceForBooth(Guid boothID, [FromBody] decimal price)
        {
            await _slotService.UpdateSlotPriceForBooth(boothID, price);
            return Ok();
        }
    }
}
