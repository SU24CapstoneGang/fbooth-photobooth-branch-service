using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public class StickerService : IStickerService
{
    private readonly IStickerRepository _stickerRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public StickerService(IStickerRepository stickerRepository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _stickerRepository = stickerRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }


    public async Task<StickerResponse> CreateStickerAsync(IFormFile file)
    {

        //upload to cloudinary
        var uploadResult = await _cloudinaryService.AddPhotoAsync(file, "FBooth-Sticker");
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        //create object from cloudinary's return 
        var sticker = new Sticker
        {
            StickerCode = file.FileName,
            stickerHeight = uploadResult.Height,
            stickerWidth = uploadResult.Width,
            StickerURL = uploadResult.SecureUrl.AbsoluteUri,
            CouldID = uploadResult.PublicId,
            Status = StatusUse.Available,
        };

        await _stickerRepository.AddAsync(sticker);

        return _mapper.Map<StickerResponse>(sticker);
    }
    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Sticker? sticker = (await _stickerRepository.GetAsync(s => s.StickerID == id)).FirstOrDefault();
            if (sticker != null)
            {
                await _stickerRepository.RemoveAsync(sticker);
                await _cloudinaryService.DeletePhotoAsync(sticker.CouldID);
            }
        }
        catch
        {
            throw new BadHttpRequestException("An error occurred while deleting the sticker");
        }
    }

    // Read
    public async Task<IEnumerable<StickerResponse>> GetAllAsync()
    {
        var stickers = await _stickerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StickerResponse>>(stickers.ToList());
    }

    public async Task<IEnumerable<StickerResponse>> GetAllPagingAsync(StickerFilter filter, PagingModel paging)
    {
        var stickers = (await _stickerRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listStickerresponse = _mapper.Map<IEnumerable<StickerResponse>>(stickers);
        return listStickerresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    public async Task<StickerResponse> GetByIdAsync(Guid id)
    {
        var sticker = (await _stickerRepository.GetAsync(s => s.StickerID == id)).FirstOrDefault();
        return _mapper.Map<StickerResponse>(sticker);
    }

    public async Task<IEnumerable<StickerResponse>> GetByName(string name)
    {
        var stickers = await _stickerRepository.GetAsync(s => s.StickerCode.Contains(name));
        return _mapper.Map<IEnumerable<StickerResponse>>(stickers.ToList());
    }

    public async Task UpdateStickerAsync(IFormFile file, Guid id, UpdateStickerRequest updateModel)
    {
        var sticker = (await _stickerRepository.GetAsync(s => s.StickerID == id)).FirstOrDefault();
        if (sticker == null)
        {
            throw new KeyNotFoundException("Sticker not found.");
        }
        var updatedSticker = _mapper.Map(updateModel, sticker);
        updatedSticker.LastModified = DateTime.UtcNow;
        await _stickerRepository.UpdateAsync(updatedSticker);
        await _cloudinaryService.UpdatePhotoAsync(file, sticker.CouldID);
    }
}

