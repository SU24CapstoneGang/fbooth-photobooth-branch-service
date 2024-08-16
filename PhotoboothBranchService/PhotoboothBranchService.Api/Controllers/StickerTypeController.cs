using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.StickerType;
using PhotoboothBranchService.Application.Services.StickerTypeServices;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/sticker-type")]
    public class StickerTypeController : ControllerBaseApi
    {
        private readonly IStickerTypeService _stickerTypeService;

        public StickerTypeController(IStickerTypeService stickerTypeService)
        {
            _stickerTypeService = stickerTypeService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<StickerTypeResponse>> CreateStickerType([FromForm] CreateStickerTypeRequest createStickerTypeRequest)
        {
            var createStickerTypeResponse = await _stickerTypeService.CreateAsync(createStickerTypeRequest);
            return Ok(createStickerTypeResponse);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StickerTypeResponse>>> GetAllStickerTypes()
        {
            var stickerTypes = await _stickerTypeService.GetAllAsync();
            return Ok(stickerTypes);
        }

        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<StickerTypeResponse>>> GetAllStickerTypes(
            [FromQuery] StickerTypeFilter stickerTypeFilter, [FromQuery] PagingModel pagingModel)
        {
            var stickerTypes = await _stickerTypeService.GetAllPagingAsync(stickerTypeFilter, pagingModel);
            return Ok(stickerTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StickerTypeResponse>> GetStickerTypeById(Guid id)
        {
            var stickerType = await _stickerTypeService.GetByIdAsync(id);
            if (stickerType == null)
            {
                return NotFound();
            }
            return Ok(stickerType);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStickerType(Guid id, [FromForm] UpdateStickerTypeRequest updateStickerTypeRequest)
        {
            await _stickerTypeService.UpdateAsync(id, updateStickerTypeRequest);
            return Ok();
        }

        // Delete
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteStickerType(Guid id)
        //{
        //    await _stickerTypeService.DeleteAsync(id);
        //    return Ok();
        //}
    }
}
