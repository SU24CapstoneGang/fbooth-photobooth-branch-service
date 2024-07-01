using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public class BoothBranchService : IBoothBranchService
{
    private readonly IBoothBranchRepository _photoBoothBranchRepository;
    private readonly IMapper _mapper;

    public BoothBranchService(IBoothBranchRepository photoBoothBranchRepository, IMapper mapper)
    {
        _photoBoothBranchRepository = photoBoothBranchRepository;
        _mapper = mapper;
    }
    //Create
    public async Task<CreateBoothBranchResponse> CreateAsync(CreateBoothBranchRequest createModel)
    {
        try
        {
            BoothBranch photoBoothBranch = _mapper.Map<BoothBranch>(createModel);
            await _photoBoothBranchRepository.AddAsync(photoBoothBranch);
            return _mapper.Map<CreateBoothBranchResponse>(photoBoothBranch);
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
            var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.BoothBranchID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<BoothBranchResponse>> GetAllAsync()
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BoothBranchResponse>>(photoBoothBranches.ToList());
    }

    public async Task<IEnumerable<BoothBranchResponse>> GetAllPagingAsync(BoothBranchFilter filter, PagingModel paging)
    {
        var photoBoothBranches = (await _photoBoothBranchRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listPhotoBoothBranchresponse = _mapper.Map<IEnumerable<BoothBranchResponse>>(photoBoothBranches);
        return listPhotoBoothBranchresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    public async Task<BoothBranchResponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.BoothBranchID == id)).FirstOrDefault();
        return _mapper.Map<BoothBranchResponse>(photoBoothBranch);
    }

    public async Task<IEnumerable<BoothBranchResponse>> GetByStatus(ManufactureStatus status)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.Status == status);
        return _mapper.Map<IEnumerable<BoothBranchResponse>>(photoBoothBranch);
    }

    public async Task<IEnumerable<BoothBranchResponse>> SearchByName(string name)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.BranchName.Contains(name));
        return _mapper.Map<IEnumerable<BoothBranchResponse>>(photoBoothBranch);
    }

    //update
    public async Task UpdateAsync(Guid id, UpdateBoothBranchRequest updateModel)
    {
        var photobranch = (await _photoBoothBranchRepository.GetAsync(p => p.BoothBranchID == id)).FirstOrDefault();
        if (photobranch == null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        var updatePhotoBoothBranch = _mapper.Map(updateModel, photobranch);
        await _photoBoothBranchRepository.UpdateAsync(updatePhotoBoothBranch);
    }
}

