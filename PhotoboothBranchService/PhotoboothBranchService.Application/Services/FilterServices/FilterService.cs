using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Filter;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

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
    public async Task<Guid> CreateAsync(CreateFilterRequest createModel)
    {
        Filter filter = _mapper.Map<Filter>(createModel);
        filter.Status = StatusUse.Available;
        return await _filterRepository.AddAsync(filter);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Filter? filter = (await _filterRepository.GetAsync(f => f.FilterID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<Filterresponse>> GetAllAsync()
    {
        var filters = await _filterRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Filterresponse>>(filters.ToList());
    }

    public async Task<IEnumerable<Filterresponse>> GetAllPagingAsync(FilterFilter filter, PagingModel paging)
    {
        var filters = (await _filterRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listFilterresponse = _mapper.Map<IEnumerable<Filterresponse>>(filters);
        listFilterresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listFilterresponse;
    }

    public async Task<Filterresponse> GetByIdAsync(Guid id)
    {
        var filter = (await _filterRepository.GetAsync(f => f.FilterID == id)).FirstOrDefault();
        return _mapper.Map<Filterresponse>(filter);
    }

    public async Task<IEnumerable<Filterresponse>> GetByName(string name)
    {
        var filters = await _filterRepository.GetAsync(f => f.FilterName.Contains(name));
        return _mapper.Map<IEnumerable<Filterresponse>>(filters.ToList());
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateFilterRequest updateModel)
    {
        var filters = await _filterRepository.GetAsync(d => d.FilterID == id);
        var filter = filters.FirstOrDefault();
        if (filter == null)
        {
            throw new KeyNotFoundException("Filter not found.");
        }

        var updatedFilter = _mapper.Map(updateModel, filter);
        updatedFilter.LastModified = DateTime.UtcNow;
        await _filterRepository.UpdateAsync(updatedFilter);
    }
}

