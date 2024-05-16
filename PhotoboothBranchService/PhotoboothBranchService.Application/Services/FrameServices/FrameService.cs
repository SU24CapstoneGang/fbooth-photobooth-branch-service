using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FrameServices;

public class FrameService : IFrameService
{
    private readonly IFrameRepository _frameRepository;
    private readonly IMapper _mapper;

    public FrameService(IFrameRepository frameRepository, IMapper mapper)
    {
        _frameRepository = frameRepository;
        _mapper = mapper;
    }

    // Create
    public async Task<Guid> CreateAsync(FrameDTO entityDTO)
    {
        Frame frame = _mapper.Map<Frame>(entityDTO);
        frame.FrameID = Guid.NewGuid();
        return await _frameRepository.AddAsync(frame);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Frame? frame = await _frameRepository.GetByIdAsync(id);
            if (frame != null)
            {
                await _frameRepository.RemoveAsync(frame);
            }
        }
        catch
        {
            throw;
        }
    }

    // Read
    public async Task<IEnumerable<FrameDTO>> GetAllAsync()
    {
        var frames = await _frameRepository.GetAll();
        return _mapper.Map<IEnumerable<FrameDTO>>(frames);
    }

    public async Task<FrameDTO> GetByIdAsync(Guid id)
    {
        var frame = await _frameRepository.GetByIdAsync(id);
        return _mapper.Map<FrameDTO>(frame);
    }

    public async Task<IEnumerable<FrameDTO>> GetByName(string name)
    {
        var frames = await _frameRepository.GetByName(name);
        return _mapper.Map<IEnumerable<FrameDTO>>(frames);
    }

    // Update
    public async Task UpdateAsync(Guid id, FrameDTO entityDTO)
    {
        entityDTO.FrameID = id;
        Frame frame = _mapper.Map<Frame>(entityDTO);
        await _frameRepository.UpdateAsync(frame);
    }
}

