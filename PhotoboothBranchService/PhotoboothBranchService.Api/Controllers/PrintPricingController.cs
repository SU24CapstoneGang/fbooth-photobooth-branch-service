using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Application.Services.PrintPricingServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrintPricingController : ControllerBase
    {
        private readonly IPrintPricingService _printPricingService;

        public PrintPricingController(IPrintPricingService printPricingService)
        {
            _printPricingService = printPricingService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePrintPricing(CreatePrintPricingRequest createPrintPricingRequest)
        {
            try
            {
                var id = await _printPricingService.CreateAsync(createPrintPricingRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the printPricing: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintPricingResponse>>> GetAllPrintPricings()
        {
            try
            {
                var printPricings = await _printPricingService.GetAllAsync();
                return Ok(printPricings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving printPricings: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PrintPricingResponse>>> GetPagingPrintPricings(
            [FromQuery] PrintPricingFilter printPricingFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var printPricings = await _printPricingService.GetAllPagingAsync(printPricingFilter, pagingModel);
                return Ok(printPricings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving printPricings: {ex.Message}");
            }
        }

        //// Read by name
        //[HttpGet("name/{name}")]
        //public async Task<ActionResult<IEnumerable<PrintPricingResponse>>> GetPrintPricingsByName(string name)
        //{
        //    try
        //    {
        //        var printPricings = await _printPricingService.GetByName(name);
        //        return Ok(printPricings);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while retrieving printPricings by name: {ex.Message}");
        //    }
        //}

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PrintPricingResponse>> GetPrintPricingById(Guid id)
        {
            try
            {
                var printPricing = await _printPricingService.GetByIdAsync(id);
                if (printPricing == null)
                {
                    return NotFound();
                }
                return Ok(printPricing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the printPricing by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrintPricing(Guid id, UpdatePrintPricingRequest updatePrintPricingRequest)
        {
            try
            {
                await _printPricingService.UpdateAsync(id, updatePrintPricingRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the printPricing: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrintPricing(Guid id)
        {
            try
            {
                await _printPricingService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the printPricing: {ex.Message}");
            }
        }
    }
}
