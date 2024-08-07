using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
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
    private readonly IBackgroundRepository _backgroundRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILayoutRepository _layoutRepository;

    public BackgroundService(IBackgroundRepository backgroundRepository, IMapper mapper, ICloudinaryService cloudinaryService, ILayoutRepository layoutRepository)
    {
        _backgroundRepository = backgroundRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _layoutRepository = layoutRepository;
    }

    public async Task<BackgroundResponse> CreateBackgroundAsync(IFormFile file, Guid layoutID)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == layoutID)).FirstOrDefault();
        if (layout != null)
        {
            //upload to cloudinary
            var uploadResult = await _cloudinaryService.AddPhotoAsync(file, "FBooth-Background");
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            //create object from cloudinary's return 
            var background = new Background
            {
                BackgroundCode = file.FileName,
                BackgroundURL = uploadResult.SecureUrl.AbsoluteUri,
                CouldID = uploadResult.PublicId,
                Status = StatusUse.Available,
                Width = uploadResult.Width,
                Height = uploadResult.Height,
                LayoutID = layoutID,
                CreatedDate = DateTimeHelper.GetVietnamTimeNow(),
            };

            await _backgroundRepository.AddAsync(background);
            return _mapper.Map<BackgroundResponse>(background);
        }
        throw new NotFoundException($"backgound not created");
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var backGround = (await _backgroundRepository.GetAsync(f => f.BackgroundID == id)).FirstOrDefault();
            if (backGround != null)
            {
                await _backgroundRepository.RemoveAsync(backGround);
                await _cloudinaryService.DeletePhotoAsync(backGround.CouldID);
            }
        }
        catch
        {
            throw new NotFoundException($"Cannot found backgound"); ;
        }
    }

    // Read
    public async Task<IEnumerable<BackgroundResponse>> GetAllAsync()
    {
        var backGrounds = await _backgroundRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BackgroundResponse>>(backGrounds.ToList().OrderByDescending(i => i.LastModified));
    }

    public async Task<IEnumerable<BackgroundResponse>> GetAllPagingAsync(BackgroundFilter filter, PagingModel paging)
    {
        var backGrounds = (await _backgroundRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listBackGroundresponse = _mapper.Map<IEnumerable<BackgroundResponse>>(backGrounds);
        return listBackGroundresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).ToList().OrderByDescending(i => i.LastModified);
    }

    public async Task<IEnumerable<BackgroundResponse>> GetAvailableAsync()
    {
        var backGrounds = (await _backgroundRepository.GetAsync(i=>i.Status == StatusUse.Available));
        return _mapper.Map<IEnumerable<BackgroundResponse>>(backGrounds.ToList().OrderByDescending(i => i.LastModified));
    }

    public async Task<BackgroundResponse> GetByIdAsync(Guid id)
    {
        var backGround = (await _backgroundRepository.GetAsync(f => f.BackgroundID == id)).FirstOrDefault();
        return _mapper.Map<BackgroundResponse>(backGround);
    }

    public async Task<IEnumerable<BackgroundResponse>> GetByName(string name)
    {
        var backGrounds = await _backgroundRepository.GetAsync(f => f.BackgroundCode.Contains(name));
        return _mapper.Map<IEnumerable<BackgroundResponse>>(backGrounds.ToList().OrderByDescending(i => i.LastModified));
    }


    public async Task UpdateBackGroundAsync(IFormFile file, Guid BackGroundID, UpdateBackgroundRequest updateBackgroundRequest)
    {
        var backGround = (await _backgroundRepository.GetAsync(f => f.BackgroundID == BackGroundID)).FirstOrDefault();
        if (backGround == null)
        {
            throw new KeyNotFoundException("BackGround not found.");
        }

        var updateBackGround = _mapper.Map(updateBackgroundRequest, backGround);
        updateBackGround.LastModified = DateTimeHelper.GetVietnamTimeNow();
        await _backgroundRepository.UpdateAsync(updateBackGround);
        await _cloudinaryService.UpdatePhotoAsync(file, backGround.CouldID);
    }
}

