using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhotoboothBranchService.Application.Service;

public class CameraService : ICameraService
{
    private readonly ICameraRepository _cameraRepository;
    private readonly IMapper _mapper;

    public CameraService(ICameraRepository cameraRepository, IMapper mapper)
    {
        _cameraRepository = cameraRepository;
        _mapper = mapper;
    }

    //Create
    public async Task<Guid> CreateAsync(CameraDTO entityDTO, CancellationToken cancellationToken)
    {
        Camera cameras = _mapper.Map<Camera>(entityDTO);
        cameras.CameraID = Guid.NewGuid();
        return await _cameraRepository.AddAsync(cameras, cancellationToken);
    }

    //Delete
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Camera? cameras = await _cameraRepository.GetByIdAsync(id,cancellationToken);
            if (cameras != null)
            {
                await _cameraRepository.RemoveAsync(cameras, cancellationToken);
            }
        }
        catch
        {
            throw;
        }
    }

    //Read
    public async Task<IEnumerable<CameraDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var cameras = await _cameraRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<CameraDTO>>(cameras);
    }

    public async Task<CameraDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cameras = await _cameraRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CameraDTO>(cameras);
    }

    public async Task<IEnumerable<CameraDTO>> GetByName(string name, CancellationToken cancellationToken)
    {
        var cameras = await _cameraRepository.GetByName(name, cancellationToken);
        return _mapper.Map<IEnumerable<CameraDTO>>(cameras);
    }


    //Update
    public async Task UpdateAsync(Guid id, CameraDTO entityDTO, CancellationToken cancellationToken)
    {
        entityDTO.CameraId= id;
        Camera cameras = _mapper.Map<Camera>(entityDTO);
        await _cameraRepository.UpdateAsync(cameras,cancellationToken);
    }
}
