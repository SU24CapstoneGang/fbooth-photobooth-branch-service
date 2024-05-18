using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public class LayoutService : ILayoutService
{
    private readonly ILayoutRepository _layoutRepository;
    private readonly IMapper _mapper;

    public LayoutService(ILayoutRepository layoutRepository, IMapper mapper)
    {
        _layoutRepository = layoutRepository;
        _mapper = mapper;
    }

    // Create
    public async Task<Guid> CreateAsync(LayoutDTO entityDTO)
    {
        Layout layout = _mapper.Map<Layout>(entityDTO);
        layout.LayoutID = Guid.NewGuid();
        return await _layoutRepository.AddAsync(layout);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Layout? layout = await _layoutRepository.GetByIdAsync(id);
            if (layout != null)
            {
                await _layoutRepository.RemoveAsync(layout);
            }
        }
        catch
        {
            throw;
        }
    }

    // Read
    public async Task<IEnumerable<LayoutDTO>> GetAllAsync()
    {
        var layouts = await _layoutRepository.GetAll();
        return _mapper.Map<IEnumerable<LayoutDTO>>(layouts);
    }

    public async Task<LayoutDTO> GetByIdAsync(Guid id)
    {
        var layout = await _layoutRepository.GetByIdAsync(id);
        return _mapper.Map<LayoutDTO>(layout);
    }

    // Update
    public async Task UpdateAsync(Guid id, LayoutDTO entityDTO)
    {
        entityDTO.LayoutID = id;
        Layout layout = _mapper.Map<Layout>(entityDTO);
        layout.LastModified = DateTime.UtcNow;
        await _layoutRepository.UpdateAsync(layout);
    }
}

