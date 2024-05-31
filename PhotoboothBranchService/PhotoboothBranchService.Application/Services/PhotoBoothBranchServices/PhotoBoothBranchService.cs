using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBoothBranch;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

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
    //Create
    public async Task<Guid> CreateAsync(CreatePhotoBoothBranchRequest createModel)
    {
        try
        {
            // await _photoBoothBranchRepository.GetAsync(createModel.)
            PhotoBoothBranch photoBoothBranch = _mapper.Map<PhotoBoothBranch>(createModel);
            await _photoBoothBranchRepository.AddAsync(photoBoothBranch);
            return photoBoothBranch.PhotoBoothBranchID;
        }
        catch (Exception)
        {
            throw;
        }
    }
    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.PhotoBoothBranchID == id)).FirstOrDefault();
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
    //read
    public async Task<IEnumerable<PhotoBoothBranchresponse>> GetAllAsync()
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PhotoBoothBranchresponse>>(photoBoothBranches.ToList());
    }

    public async Task<IEnumerable<PhotoBoothBranchresponse>> GetAllPagingAsync(PhotoBoothBranchFilter filter, PagingModel paging)
    {
        var photoBoothBranches = (await _photoBoothBranchRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listPhotoBoothBranchresponse = _mapper.Map<IEnumerable<PhotoBoothBranchresponse>>(photoBoothBranches);
        listPhotoBoothBranchresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listPhotoBoothBranchresponse;
    }

    public async Task<PhotoBoothBranchresponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.PhotoBoothBranchID == id)).FirstOrDefault();
        return _mapper.Map<PhotoBoothBranchresponse>(photoBoothBranch);
    }

    public async Task<IEnumerable<PhotoBoothBranchresponse>> GetByStatus(ManufactureStatus status)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.Status == status);
        return _mapper.Map<IEnumerable<PhotoBoothBranchresponse>>(photoBoothBranch);
    }

    public async Task<IEnumerable<PhotoBoothBranchresponse>> SearchByName(string name)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.BranchName.Contains(name));
        return _mapper.Map<IEnumerable<PhotoBoothBranchresponse>>(photoBoothBranch);
    }

    //update
    public async Task UpdateAsync(Guid id, UpdatePhotoBoothBranchRequest updateModel)
    {
        var photobranch = (await _photoBoothBranchRepository.GetAsync(p => p.PhotoBoothBranchID == id)).FirstOrDefault();
        if (photobranch == null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        var updatePhotoBoothBranch = _mapper.Map(updateModel, photobranch);
        await _photoBoothBranchRepository.UpdateAsync(updatePhotoBoothBranch);
    }
}

