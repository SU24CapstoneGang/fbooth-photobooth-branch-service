using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

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
    public async Task<Guid> CreateAsync(CreateFrameRequest createModel)
    {
        Frame frame = _mapper.Map<Frame>(createModel);
        frame.Status = StatusUse.Available;
        return await _frameRepository.AddAsync(frame);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Frame? frame = (await _frameRepository.GetAsync(f => f.FrameID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<FrameResponse>> GetAllAsync()
    {
        var frames = await _frameRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FrameResponse>>(frames.ToList());
    }

    public async Task<IEnumerable<FrameResponse>> GetAllPagingAsync(FrameFilter filter, PagingModel paging)
    {
        var frames = (await _frameRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listFrameresponse = _mapper.Map<IEnumerable<FrameResponse>>(frames);
        listFrameresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listFrameresponse;
    }

    public async Task<FrameResponse> GetByIdAsync(Guid id)
    {
        var frame = (await _frameRepository.GetAsync(f => f.FrameID == id)).FirstOrDefault();
        return _mapper.Map<FrameResponse>(frame);
    }

    public async Task<IEnumerable<FrameResponse>> GetByName(string name)
    {
        var frames = await _frameRepository.GetAsync(f => f.FrameName.Contains(name));
        return _mapper.Map<IEnumerable<FrameResponse>>(frames.ToList());
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateFrameRequest updateModel)
    {
        var frame = (await _frameRepository.GetAsync(f => f.FrameID == id)).FirstOrDefault();
        if (frame == null)
        {
            throw new KeyNotFoundException("Printer not found.");
        }

        var updateFrame = _mapper.Map(updateModel, frame);
        await _frameRepository.UpdateAsync(updateFrame);
    }
}

