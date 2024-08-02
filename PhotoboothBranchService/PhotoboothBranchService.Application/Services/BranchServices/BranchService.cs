using AutoMapper;
using CloudinaryDotNet;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Security.AccessControl;

namespace PhotoboothBranchService.Application.Services.BranchServices;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public BranchService(IBranchRepository branchRepository, IMapper mapper, IAccountRepository accountRepository)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }
    //Create
    public async Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel, BranchStatus status)
    {
        try
        {
            //validate input
            if (createModel.ManagerID.HasValue)
            {
                await ValideManagerForBranch(createModel.ManagerID.Value);
            }
            Branch branch = _mapper.Map<Branch>(createModel);
            branch.Status = status;
            branch.CreateDate = DateTimeHelper.GetVietnamTimeNow();
            await _branchRepository.AddAsync(branch);
            return _mapper.Map<CreateBranchResponse>(branch);
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
            var branch = (await _branchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
            if (branch != null)
            {
                await _branchRepository.RemoveAsync(branch);
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
        var branches = await _branchRepository.GetAsync(null, bth => bth.Booths);
        return _mapper.Map<IEnumerable<BranchResponse>>(branches.ToList());
    }

    public async Task<IEnumerable<BranchResponse>> GetAllPagingAsync(BranchFilter filter, PagingModel paging)
    {
        var branches = (await _branchRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listBranchresponse = _mapper.Map<IEnumerable<BranchResponse>>(branches);
        return listBranchresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    public async Task<BranchResponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _branchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
        return _mapper.Map<BranchResponse>(photoBoothBranch);
    }

    public async Task<IEnumerable<BranchResponse>> GetByStatus(BranchStatus status)
    {
        var photoBoothBranch = await _branchRepository.GetAsync(p => p.Status == status);
        return _mapper.Map<IEnumerable<BranchResponse>>(photoBoothBranch);
    }

    public async Task<IEnumerable<BranchResponse>> SearchByName(string name)
    {
        var branchs = await _branchRepository.GetAsync(p => p.BranchName.Contains(name));
        return _mapper.Map<IEnumerable<BranchResponse>>(branchs);
    }

    //update
    public async Task UpdateAsync(Guid id, UpdateBranchRequest updateModel, BranchStatus? status)
    {
        var branch = (await _branchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
        if (branch == null)
        {
            throw new NotFoundException("Branch not found.");
        }

        var updateBranch = _mapper.Map(updateModel, branch);
        if (updateModel.ManagerID.HasValue)
        {
            await ValideManagerForBranch(id);
        }
        if (status.HasValue)
        {
            updateBranch.Status = status.Value;
        }
        await _branchRepository.UpdateAsync(updateBranch);
    }

    public async Task AssignManager(Guid branchId, AssignManagerRequest request)
    {
        await UpdateAsync(branchId, new UpdateBranchRequest { ManagerID = request.ManagerID }, null);
    }

    private async Task ValideManagerForBranch(Guid managerId)
    {
        var branchCheck = (await _branchRepository.GetAsync(i => i.ManagerID == managerId)).FirstOrDefault();
        if (branchCheck != null)
        {
            throw new BadRequestException("This manager is in another branch");
        }
        var account = (await _accountRepository.GetAsync(i => i.AccountID == managerId)).FirstOrDefault();
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
    }
}

