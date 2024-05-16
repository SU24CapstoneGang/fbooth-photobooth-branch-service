using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public async Task<Guid> CreateAsync(StickerDTO entityDTO)
    {
        Sticker sticker = _mapper.Map<Sticker>(entityDTO);
        sticker.StickerId = Guid.NewGuid();
        return await _stickerRepository.AddAsync(sticker);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Sticker? sticker = await _stickerRepository.GetByIdAsync(id);
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
    public async Task<IEnumerable<StickerDTO>> GetAllAsync()
    {
        var stickers = await _stickerRepository.GetAll();
        return _mapper.Map<IEnumerable<StickerDTO>>(stickers);
    }

    public async Task<StickerDTO> GetByIdAsync(Guid id)
    {
        var sticker = await _stickerRepository.GetByIdAsync(id);
        return _mapper.Map<StickerDTO>(sticker);
    }

    public async Task<IEnumerable<StickerDTO>> GetByName(string name)
    {
        var stickers = await _stickerRepository.GetByName(name);
        return _mapper.Map<IEnumerable<StickerDTO>>(stickers);
    }

    // Update
    public async Task UpdateAsync(Guid id, StickerDTO entityDTO)
    {
        entityDTO.StickerId = id;
        Sticker sticker = _mapper.Map<Sticker>(entityDTO);
        sticker.LastModified = DateTime.UtcNow;
        await _stickerRepository.UpdateAsync(sticker);
    }
}

