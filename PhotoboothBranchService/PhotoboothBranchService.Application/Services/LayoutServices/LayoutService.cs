using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Layout;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Layout;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

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
    public async Task<Guid> CreateAsync(CreateLayoutRequest createModel)
    {
        Layout layout = _mapper.Map<Layout>(createModel);
        return await _layoutRepository.AddAsync(layout);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Layout? layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<Layoutresponse>> GetAllAsync()
    {
        var layouts = await _layoutRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Layoutresponse>>(layouts.ToList());
    }

    public async Task<IEnumerable<Layoutresponse>> GetAllPagingAsync(LayoutFilter filter, PagingModel paging)
    {
        var layouts = (await _layoutRepository.GetAllAsync()).AutoPaging(paging.PageSize,paging.PageIndex);
        var listLayoutresponse = _mapper.Map<IEnumerable<Layoutresponse>>(layouts.ToList());
        listLayoutresponse.AutoFilter(filter);
        return listLayoutresponse;
    }

    public async Task<Layoutresponse> GetByIdAsync(Guid id)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
        return _mapper.Map<Layoutresponse>(layout);
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateLayoutRequest updateMdel)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
        if (layout == null)
        {
            throw new KeyNotFoundException("Layout not found.");
        }

        var updateLayout = _mapper.Map(updateMdel, layout);
        updateLayout.LastModified = DateTime.UtcNow;
        await _layoutRepository.UpdateAsync(updateLayout);
    }
}

