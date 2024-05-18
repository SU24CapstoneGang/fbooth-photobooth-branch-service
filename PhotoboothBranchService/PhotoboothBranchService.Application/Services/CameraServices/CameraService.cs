using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhotoboothBranchService.Application.Services.CameraServices;

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
    public async Task<Guid> CreateAsync(CameraDTO entityDTO)
    {
        Camera cameras = _mapper.Map<Camera>(entityDTO);
        cameras.CameraID = Guid.NewGuid();
        return await _cameraRepository.AddAsync(cameras);
    }

    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Camera? cameras = await _cameraRepository.GetByIdAsync(id);
            if (cameras != null)
            {
                await _cameraRepository.RemoveAsync(cameras);
            }
        }
        catch
        {
            throw;
        }
    }

    //Read
    public async Task<IEnumerable<CameraDTO>> GetAllAsync()
    {
        var cameras = await _cameraRepository.GetAll();
        return _mapper.Map<IEnumerable<CameraDTO>>(cameras);
    }

    public async Task<CameraDTO> GetByIdAsync(Guid id)
    {
        var cameras = await _cameraRepository.GetByIdAsync(id);
        return _mapper.Map<CameraDTO>(cameras);
    }

    public async Task<IEnumerable<CameraDTO>> GetByName(string name)
    {
        var cameras = await _cameraRepository.GetByName(name);
        return _mapper.Map<IEnumerable<CameraDTO>>(cameras);
    }


    //Update
    public async Task UpdateAsync(Guid id, CameraDTO entityDTO)
    {
        entityDTO.CameraId = id;
        Camera cameras = _mapper.Map<Camera>(entityDTO);
        await _cameraRepository.UpdateAsync(cameras);
    }
}
