using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;

public class PhotoBoothBranchService : IPhotoBoothBranchService
{
    private readonly IPhotoBoothBranchRepository _photoBoothBranchRepository;
    private readonly IMapper _mapper;

    public PhotoBoothBranchService(IPhotoBoothBranchRepository photoBoothBranchRepository, IMapper mapper)
    {
        _photoBoothBranchRepository = photoBoothBranchRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(PhotoBoothBranchDTO entityDTO)
    {
        PhotoBoothBranch photoBoothBranch = _mapper.Map<PhotoBoothBranch>(entityDTO);
        photoBoothBranch.BranchesID = Guid.NewGuid();
        await _photoBoothBranchRepository.AddAsync(photoBoothBranch);
        return photoBoothBranch.BranchesID;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var photoBoothBranch = await _photoBoothBranchRepository.GetByIdAsync(id);
            if (photoBoothBranch != null)
            {
                await _photoBoothBranchRepository.RemoveAsync(photoBoothBranch);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetAll(ManufactureStatus status)
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAll(status);
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetAllAsync()
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAll();
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task<PhotoBoothBranchDTO> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetByIdAsync(id);
        return _mapper.Map<PhotoBoothBranchDTO>(photoBoothBranch);
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetByName(string name)
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetByName(name);
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task UpdateAsync(Guid id, PhotoBoothBranchDTO entityDTO)
    {
        entityDTO.PhotoBoothBranchId = id;
        PhotoBoothBranch photoBoothBranch = _mapper.Map<PhotoBoothBranch>(entityDTO);
        await _photoBoothBranchRepository.UpdateAsync(photoBoothBranch);
    }
}

