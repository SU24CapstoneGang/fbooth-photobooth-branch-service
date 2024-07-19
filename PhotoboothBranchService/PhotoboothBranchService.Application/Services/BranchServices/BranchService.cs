using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _photoBoothBranchRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public BranchService(IBranchRepository photoBoothBranchRepository, IMapper mapper, IAccountRepository accountRepository)
    {
        _photoBoothBranchRepository = photoBoothBranchRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }
    //Create
    public async Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel)
    {
        try
        {
            //validate input
            var brnach = (await _photoBoothBranchRepository.GetAsync(i => i.ManagerID == createModel.ManagerID)).FirstOrDefault();
            if (brnach != null) {
                throw new BadRequestException("This manager is in another branch");
            }
            var account = (await _accountRepository.GetAsync(i => i.AccountID == createModel.ManagerID)).FirstOrDefault();
            if (account == null)
            {
                throw new NotFoundException("Not found Account");
            }
            if (account.Role != AccountRole.Manager)
            {
                throw new BadRequestException("Account assign is not manager");
            }
            if (account.Status != AccountStatus.Active)
            {
                throw new BadRequestException("Account is not active in system");
            }
            Branch photoBoothBranch = _mapper.Map<Branch>(createModel);
            await _photoBoothBranchRepository.AddAsync(photoBoothBranch);
            return _mapper.Map<CreateBranchResponse>(photoBoothBranch);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                throw ex.InnerException;
            }
            else
            {
                throw new Exception(ex.Message);
            }
        }
    }
    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
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
    public async Task<IEnumerable<BranchResponse>> GetAllAsync()
    {
        var photoBoothBranches = await _photoBoothBranchRepository.GetAsync(null, bth => bth.Booths);
        return _mapper.Map<IEnumerable<BranchResponse>>(photoBoothBranches.ToList());
    }

    public async Task<IEnumerable<BranchResponse>> GetAllPagingAsync(BranchFilter filter, PagingModel paging)
    {
        var photoBoothBranches = (await _photoBoothBranchRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listPhotoBoothBranchresponse = _mapper.Map<IEnumerable<BranchResponse>>(photoBoothBranches);
        return listPhotoBoothBranchresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    public async Task<BranchResponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _photoBoothBranchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
        return _mapper.Map<BranchResponse>(photoBoothBranch);
    }

    public async Task<IEnumerable<BranchResponse>> GetByStatus(BranchStatus status)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.Status == status);
        return _mapper.Map<IEnumerable<BranchResponse>>(photoBoothBranch);
    }

    public async Task<IEnumerable<BranchResponse>> SearchByName(string name)
    {
        var photoBoothBranch = await _photoBoothBranchRepository.GetAsync(p => p.BranchName.Contains(name));
        return _mapper.Map<IEnumerable<BranchResponse>>(photoBoothBranch);
    }

    //update
    public async Task UpdateAsync(Guid id, UpdateBranchRequest updateModel)
    {
        var photobranch = (await _photoBoothBranchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
        if (photobranch == null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        var updatePhotoBoothBranch = _mapper.Map(updateModel, photobranch);
        await _photoBoothBranchRepository.UpdateAsync(updatePhotoBoothBranch);
    }
}

