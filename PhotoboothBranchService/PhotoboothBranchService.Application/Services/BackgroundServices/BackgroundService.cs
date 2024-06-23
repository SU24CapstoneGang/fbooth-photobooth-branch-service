using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BackgroundServices;

public class BackgroundService : IBackgroundService
{
    private readonly IBackgroundRepository _frameRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    public BackgroundService(IBackgroundRepository frameRepository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _frameRepository = frameRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    // Create
    public async Task<CreateBackgroundResponse> CreateAsync(CreateBackgroundRequest createModel)
    {
        Background frame = _mapper.Map<Background>(createModel);
        frame.Status = StatusUse.Available;
        await _frameRepository.AddAsync(frame);
        return _mapper.Map<CreateBackgroundResponse>(frame);
    }
    public async Task<BackgroundResponse> CreateBackgroundAsync(IFormFile file, CreateBackgroundRequest createModel)
    {

        //upload to cloudinary
        var uploadResult = await _cloudinaryService.AddPhotoAsync(file);
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        //create object from cloudinary's return 
        var frame = _mapper.Map<Background>(createModel);

        frame.BackgroundURL = uploadResult.SecureUrl.AbsoluteUri;
        frame.CouldID = uploadResult.PublicId;
        frame.Status = StatusUse.Available;

        await _frameRepository.AddAsync(frame);

        return _mapper.Map<BackgroundResponse>(frame);
    }
    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Background? frame = (await _frameRepository.GetAsync(f => f.BackgroundID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<BackgroundResponse>> GetAllAsync()
    {
        var frames = await _frameRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BackgroundResponse>>(frames.ToList());
    }

    public async Task<IEnumerable<BackgroundResponse>> GetAllPagingAsync(BackgroundFilter filter, PagingModel paging)
    {
        var frames = (await _frameRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listFrameresponse = _mapper.Map<IEnumerable<BackgroundResponse>>(frames);
        listFrameresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listFrameresponse;
    }

    public async Task<BackgroundResponse> GetByIdAsync(Guid id)
    {
        var frame = (await _frameRepository.GetAsync(f => f.BackgroundID == id)).FirstOrDefault();
        return _mapper.Map<BackgroundResponse>(frame);
    }

    public async Task<IEnumerable<BackgroundResponse>> GetByName(string name)
    {
        var frames = await _frameRepository.GetAsync(f => f.BackgroundCode.Contains(name));
        return _mapper.Map<IEnumerable<BackgroundResponse>>(frames.ToList());
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateBackgroundRequest updateModel)
    {
        var frame = (await _frameRepository.GetAsync(f => f.BackgroundID == id)).FirstOrDefault();
        if (frame == null)
        {
            throw new KeyNotFoundException("Frame not found.");
        }

        var updateFrame = _mapper.Map(updateModel, frame);
        await _frameRepository.UpdateAsync(updateFrame);
    }
}

