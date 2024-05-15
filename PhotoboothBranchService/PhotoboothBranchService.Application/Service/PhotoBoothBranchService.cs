using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Service;

public class PhotoBoothBranchService : IPhotoBoothBranchService
{
    private readonly IPhotoBoothBranchRepository _photoBoothBranchRepository;
    private readonly IMapper _mapper;

    public PhotoBoothBranchService(IPhotoBoothBranchRepository photoBoothBranchRepository, IMapper mapper)
    {
        _photoBoothBranchRepository = photoBoothBranchRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(PhotoBoothBranchDTO entityDTO, CancellationToken cancellationToken)
    {
        PhotoBoothBranch photoBoothBranch = _mapper.Map<PhotoBoothBranch>(entityDTO);
        photoBoothBranch.BranchesID = Guid.NewGuid();
        await _photoBoothBranchRepository.AddAsync(photoBoothBranch, cancellationToken);
        return photoBoothBranch.BranchesID;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var photoBoothBranch = await _photoBoothBranchRepository.GetByIdAsync(id, cancellationToken);
            if (photoBoothBranch != null)
            {
                await _photoBoothBranchRepository.RemoveAsync(photoBoothBranch, cancellationToken);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetAll(ManufactureStatus status, CancellationToken cancellationToken)
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAll(status, cancellationToken);
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task<PhotoBoothBranchDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<PhotoBoothBranchDTO>(photoBoothBranch);
    }

    public async Task<IEnumerable<PhotoBoothBranchDTO>> GetByName(string name, CancellationToken cancellationToken)
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetByName(name, cancellationToken);
        return _mapper.Map<IEnumerable<PhotoBoothBranchDTO>>(photoBoothBranches);
    }

    public async Task UpdateAsync(Guid id, PhotoBoothBranchDTO entityDTO, CancellationToken cancellationToken)
    {
        entityDTO.PhotoBoothBranchId = id;
        PhotoBoothBranch photoBoothBranch = _mapper.Map<PhotoBoothBranch>(entityDTO);
        await _photoBoothBranchRepository.UpdateAsync(photoBoothBranch, cancellationToken);
    }
}

