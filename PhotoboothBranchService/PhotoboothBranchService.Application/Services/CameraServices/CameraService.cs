using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Camera;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

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
    public async Task<Guid> CreateAsync(CreateCameraRequest createModel)
    {

        try
        {
            Camera cameras = _mapper.Map<Camera>(createModel);
            cameras.Status = ManufactureStatus.Active;
            return await _cameraRepository.AddAsync(cameras);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while create the account: " + ex.Message);
        }
    }
    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var cameras = await _cameraRepository.GetAsync(c => c.CameraID == id);
            var camera = cameras.FirstOrDefault();
            if (camera == null)
            {
                throw new NotFoundException("Camera", id, "Camera id not found");
            }
            await _cameraRepository.RemoveAsync(camera);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the camera: " + ex.Message);
        }
    }
    //Read
    public async Task<IEnumerable<Cameraresponse>> GetAllAsync()
    {
        try
        {
            var cameras = await _cameraRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Cameraresponse>>(cameras.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the camera: " + ex.Message);
        }
    }

    public async Task<IEnumerable<Cameraresponse>> GetAllPagingAsync(CameraFilter filter, PagingModel paging)
    {
        try
        {
            var cameras = (await _cameraRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listCameraresponse = _mapper.Map<IEnumerable<Cameraresponse>>(cameras);
            listCameraresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listCameraresponse;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the camera: " + ex.Message);
        }
    }

    public async Task<Cameraresponse> GetByIdAsync(Guid id)
    {
        try
        {
            var cameras = (await _cameraRepository.GetAsync(c => c.CameraID == id)).FirstOrDefault();
            return _mapper.Map<Cameraresponse>(cameras);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the camera: " + ex.Message);
        }
    }

    public async Task<IEnumerable<Cameraresponse>> GetByName(string name)
    {
        try
        {
            var cameras = await _cameraRepository.GetAsync(c => c.ModelName.Contains(name));
            return _mapper.Map<IEnumerable<Cameraresponse>>(cameras.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the camera: " + ex.Message);
        }
    }

    //Update
    public async Task UpdateAsync(Guid id, UpdateCameraRequest updateModel)
    {
        try
        {
            var camera = (await _cameraRepository.GetAsync(c => c.CameraID == id)).FirstOrDefault();
            if (camera == null)
            {
                throw new NotFoundException("Camera", id, "Camera id not found");
            }

            var updateCamera = _mapper.Map(updateModel, camera);
            await _cameraRepository.UpdateAsync(updateCamera);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while update the camera: " + ex.Message);
        }

    }

}
