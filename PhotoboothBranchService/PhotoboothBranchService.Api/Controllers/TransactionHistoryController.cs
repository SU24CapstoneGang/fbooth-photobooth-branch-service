using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Application.Services.TransactionHistoryServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTransactionHistory(CreateTransactionHistoryRequest createTransactionHistoryRequest)
        {
            try
            {
                var id = await _transactionHistoryService.CreateAsync(createTransactionHistoryRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the transactionHistory: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionHistoryResponse>>> GetAllTransactionHistorys()
        {
            try
            {
                var transactionHistorys = await _transactionHistoryService.GetAllAsync();
                return Ok(transactionHistorys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving transactionHistorys: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<TransactionHistoryResponse>>> GetPagingTransactionHistorys(
            [FromQuery] TransactionHistoryFilter transactionHistoryFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var transactionHistorys = await _transactionHistoryService.GetAllPagingAsync(transactionHistoryFilter, pagingModel);
                return Ok(transactionHistorys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving transactionHistorys: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionHistoryResponse>> GetTransactionHistoryById(Guid id)
        {
            try
            {
                var transactionHistory = await _transactionHistoryService.GetByIdAsync(id);
                if (transactionHistory == null)
                {
                    return NotFound();
                }
                return Ok(transactionHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the transactionHistory by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransactionHistory(Guid id, UpdateTransactionHistoryRequest updateTransactionHistoryRequest)
        {
            try
            {
                await _transactionHistoryService.UpdateAsync(id, updateTransactionHistoryRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the transactionHistory: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransactionHistory(Guid id)
        {
            try
            {
                await _transactionHistoryService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the transactionHistory: {ex.Message}");
            }
        }
    }
}
