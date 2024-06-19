using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.FrameServices;

public class FrameService : IFrameService
{
    private readonly IFrameRepository _frameRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    public FrameService(IFrameRepository frameRepository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _frameRepository = frameRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    // Create
    public async Task<Guid> CreateAsync(CreateFrameRequest createModel)
    {
        Frame frame = _mapper.Map<Frame>(createModel);
        frame.Status = StatusUse.Available;
        return await _frameRepository.AddAsync(frame);
    }
    public async Task<FrameResponse> CreateFrameAsync(IFormFile file, CreateFrameRequest createModel)
    {

        //upload to cloudinary
        var uploadResult = await _cloudinaryService.AddPhotoAsync(file);
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        //create object from cloudinary's return 
        var frame = _mapper.Map<Frame>(createModel);

        frame.FrameURL = uploadResult.SecureUrl.AbsoluteUri;
        frame.CouldID = uploadResult.PublicId;
        frame.Status = StatusUse.Available;

        await _frameRepository.AddAsync(frame);

        return _mapper.Map<FrameResponse>(frame);
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
            throw new KeyNotFoundException("Frame not found.");
        }

        var updateFrame = _mapper.Map(updateModel, frame);
        await _frameRepository.UpdateAsync(updateFrame);
    }
}

