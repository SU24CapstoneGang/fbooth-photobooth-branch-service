using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs.FullPaymentPolicy;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.Services.FullPaymentPolicyServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PolicyController : ControllerBaseApi
    {
        private readonly IFullPaymentPolicyServices _policyServices;

        public PolicyController(IFullPaymentPolicyServices policyServices)
        {
            _policyServices = policyServices;
        }

        // Get all policies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullPaymentPolicyResponse>>> GetAllPolicies()
        {
            try
            {
                var policies = await _policyServices.GetAllAsync();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving policies: {ex.Message}");
            }
        }

        // Get policies with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<FullPaymentPolicyResponse>>> GetPagingPolicies(
            [FromQuery] FullPaymentPolicyFilter filter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var policies = await _policyServices.GetAllPagingAsync(filter, pagingModel);
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving policies: {ex.Message}");
            }
        }

        // Get policy by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FullPaymentPolicyResponse>> GetPolicyById(Guid id)
        {
            try
            {
                var policy = await _policyServices.GetByIdAsync(id);
                if (policy == null)
                {
                    return NotFound();
                }
                return Ok(policy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the policy by ID: {ex.Message}");
            }
        }

        // Create policy
        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyRequestModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var policy = await _policyServices.CreatePolicy(request);
                return CreatedAtAction(nameof(GetPolicyById), new { id = policy.FullPaymentPolicyID }, policy);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"An error occurred while creating the policy: {ex.Message}");
            }
        }

        // Update policy
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(Guid id, [FromQuery] UpdatePolicyRequestModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _policyServices.UpdatePolicyAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"An error occurred while updating the policy: {ex.Message}");
            }
        }

        // Delete policy
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(Guid id)
        {
            try
            {
                await _policyServices.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"An error occurred while deleting the policy: {ex.Message}");
            }
        }
    }
}
