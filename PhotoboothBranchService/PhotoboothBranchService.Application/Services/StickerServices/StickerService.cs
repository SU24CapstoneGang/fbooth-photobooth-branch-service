using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Sticker;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Sticker;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public class StickerService : IStickerService
{
    private readonly IStickerRepository _stickerRepository;
    private readonly IMapper _mapper;

    public StickerService(IStickerRepository stickerRepository, IMapper mapper)
    {
        _stickerRepository = stickerRepository;
        _mapper = mapper;
    }

    // Create
    public async Task<Guid> CreateAsync(CreateStickerRequest createModel)
    {
        Sticker sticker = _mapper.Map<Sticker>(createModel);
        return await _stickerRepository.AddAsync(sticker);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Sticker? sticker = (await _stickerRepository.GetAsync(s => s.StickerId == id)).FirstOrDefault();
            if (sticker != null)
            {
                await _stickerRepository.RemoveAsync(sticker);
            }
        }
        catch
        {
            throw;
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
        var stickers = (await _stickerRepository.GetAllAsync()).AutoPaging(paging.PageSize,paging.PageIndex);
        var listStickerresponse = _mapper.Map<IEnumerable<StickerResponse>>(stickers.ToList());
        listStickerresponse.AutoFilter(filter);
        return listStickerresponse;
    }

    public async Task<StickerResponse> GetByIdAsync(Guid id)
    {
        var sticker = (await _stickerRepository.GetAsync(s => s.StickerId == id)).FirstOrDefault();
        return _mapper.Map<StickerResponse>(sticker);
    }

    public async Task<IEnumerable<StickerResponse>> GetByName(string name)
    {
        var stickers = await _stickerRepository.GetAsync(s => s.StickerName.Contains(name));
        return _mapper.Map<IEnumerable<StickerResponse>>(stickers.ToList());
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateStickerRequest updateModel)
    {
        var sticker = (await _stickerRepository.GetAsync(s => s.StickerId == id)).FirstOrDefault();
        if (sticker == null)
        {
            throw new KeyNotFoundException("Sticker not found.");
        }
        var updatedSticker = _mapper.Map(updateModel, sticker);
        updatedSticker.LastModified = DateTime.UtcNow;
        await _stickerRepository.UpdateAsync(updatedSticker);
    }
}

