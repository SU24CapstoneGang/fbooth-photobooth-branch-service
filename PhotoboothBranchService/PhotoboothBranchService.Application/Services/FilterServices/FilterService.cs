using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FilterServices;

public class FilterService : IFilterService
{
    private readonly IFilterRepository _filterRepository;
    private readonly IMapper _mapper;

    public FilterService(IFilterRepository filterRepository, IMapper mapper)
    {
        _filterRepository = filterRepository;
        _mapper = mapper;
    }

    // Create
    public async Task<Guid> CreateAsync(FilterDTO entityDTO)
    {
        Filter filter = _mapper.Map<Filter>(entityDTO);
        filter.FilterID = Guid.NewGuid();
        return await _filterRepository.AddAsync(filter);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Filter? filter = await _filterRepository.GetByIdAsync(id);
            if (filter != null)
            {
                await _filterRepository.RemoveAsync(filter);
            }
        }
        catch
        {
            throw;
        }
    }

    // Read
    public async Task<IEnumerable<FilterDTO>> GetAllAsync()
    {
        var filters = await _filterRepository.GetAll();
        return _mapper.Map<IEnumerable<FilterDTO>>(filters);
    }

    public async Task<FilterDTO> GetByIdAsync(Guid id)
    {
        var filter = await _filterRepository.GetByIdAsync(id);
        return _mapper.Map<FilterDTO>(filter);
    }

    public async Task<IEnumerable<FilterDTO>> GetByName(string name)
    {
        var filters = await _filterRepository.GetByName(name);
        return _mapper.Map<IEnumerable<FilterDTO>>(filters);
    }

    // Update
    public async Task UpdateAsync(Guid id, FilterDTO entityDTO)
    {
        entityDTO.FilterID = id;
        Filter filter = _mapper.Map<Filter>(entityDTO);
        await _filterRepository.UpdateAsync(filter);
    }
}

