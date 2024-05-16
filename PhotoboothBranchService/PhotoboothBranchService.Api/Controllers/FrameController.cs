﻿using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;

namespace PhotoboothBranchService.Api.Controllers;

public class FrameController : ControllerBaseApi
{
    private readonly IFrameService _frameService;

    public FrameController(IFrameService frameService)
    {
        _frameService = frameService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFrame(FrameDTO frameDTO)
    {
        try
        {
            var id = await _frameService.CreateAsync(frameDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the frame: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FrameDTO>>> GetAllFrames()
    {
        try
        {
            var frames = await _frameService.GetAllAsync();
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving frames: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<FrameDTO>>> GetFramesByName(string name)
    {
        try
        {
            var frames = await _frameService.GetByName(name);
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving frames by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FrameDTO>> GetFrameById(Guid id)
    {
        try
        {
            var frame = await _frameService.GetByIdAsync(id);
            if (frame == null)
            {
                return NotFound();
            }
            return Ok(frame);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the frame by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFrame(Guid id, FrameDTO frameDTO)
    {
        try
        {
            await _frameService.UpdateAsync(id, frameDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the frame: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFrame(Guid id)
    {
        try
        {
            await _frameService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the frame: {ex.Message}");
        }
    }
}
