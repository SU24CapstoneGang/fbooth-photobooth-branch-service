using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.EffectsPackLog;
using PhotoboothBranchService.Application.Services.EffectsPackLogServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EffectsPackLogController : ControllerBase
    {
        private readonly IEffectsPackLogService _effectsPackLogService;

        public EffectsPackLogController(IEffectsPackLogService effectsPackLogService)
        {
            _effectsPackLogService = effectsPackLogService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEffectsPackLog(CreateEffectsPackLogRequest createEffectsPackLogRequest)
        {
            try
            {
                var id = await _effectsPackLogService.CreateAsync(createEffectsPackLogRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the effectsPackLog: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EffectsPackLogResponse>>> GetAllEffectsPackLogs()
        {
            try
            {
                var effectsPackLogs = await _effectsPackLogService.GetAllAsync();
                return Ok(effectsPackLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving effectsPackLogs: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<EffectsPackLogResponse>>> GetPagingEffectsPackLogs(
            [FromQuery] EffectsPackLogFilter effectsPackLogFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var effectsPackLogs = await _effectsPackLogService.GetAllPagingAsync(effectsPackLogFilter, pagingModel);
                return Ok(effectsPackLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving effectsPackLogs: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EffectsPackLogResponse>> GetEffectsPackLogById(Guid id)
        {
            try
            {
                var effectsPackLog = await _effectsPackLogService.GetByIdAsync(id);
                if (effectsPackLog == null)
                {
                    return NotFound();
                }
                return Ok(effectsPackLog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the effectsPackLog by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEffectsPackLog(Guid id, UpdateEffectsPackLogRequest updateEffectsPackLogRequest)
        {
            try
            {
                await _effectsPackLogService.UpdateAsync(id, updateEffectsPackLogRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the effectsPackLog: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEffectsPackLog(Guid id)
        {
            try
            {
                await _effectsPackLogService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the effectsPackLog: {ex.Message}");
            }
        }
    }
}
