using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Camera;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Camera;
using PhotoboothBranchService.Domain.Entities;
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
        Camera cameras = _mapper.Map<Camera>(createModel);
        return await _cameraRepository.AddAsync(cameras);
    }
    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var cameras = await _cameraRepository.GetAsync(c => c.CameraID == id);
            var camera = cameras.FirstOrDefault();
            if (camera != null)
            {
                await _cameraRepository.RemoveAsync(camera);
            }
        }
        catch
        {
            throw;
        }
    }
    //Read
    public async Task<IEnumerable<Cameraresponse>> GetAllAsync()
    {
        var cameras = await _cameraRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Cameraresponse>>(cameras.ToList());
    }

    public async Task<IEnumerable<Cameraresponse>> GetAllPagingAsync(CameraFilter filter, PagingModel paging)
    {
        var cameras = (await _cameraRepository.GetAllAsync()).AutoPaging(paging.PageSize, paging.PageIndex);
        var listCameraresponse =  _mapper.Map<IEnumerable<Cameraresponse>>(cameras.ToList());
        listCameraresponse.AutoFilter(filter);
        return listCameraresponse;
    }

    public async Task<Cameraresponse> GetByIdAsync(Guid id)
    {
        var cameras = await _cameraRepository.GetAsync(c => c.CameraID == id);
        return _mapper.Map<Cameraresponse>(cameras);
    }

    public async Task<IEnumerable<Cameraresponse>> GetByName(string name)
    {
        var cameras = await _cameraRepository.GetAsync(c => c.ModelName.Equals(name));
        return _mapper.Map<IEnumerable<Cameraresponse>>(cameras.ToList());
    }

    //Update
    public async Task UpdateAsync(Guid id, UpdateCameraRequest updateModel)
    {
        var camera = (await _cameraRepository.GetAsync(c => c.CameraID == id)).FirstOrDefault();
        if (camera == null)
        {
            throw new KeyNotFoundException("Printer not found.");
        }

        var updateCamera = _mapper.Map(updateModel, camera);
        await _cameraRepository.UpdateAsync(updateCamera);
    }

}
