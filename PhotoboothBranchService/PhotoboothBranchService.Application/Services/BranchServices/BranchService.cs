using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
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
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IBranchPhotoRepository _branchPhotoRepository;
    private readonly IMapper _mapper;

    public BranchService(IBranchRepository branchRepository, IMapper mapper, IAccountRepository accountRepository, 
        ICloudinaryService cloudinaryService, IBranchPhotoRepository branchPhotoRepository)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _cloudinaryService = cloudinaryService;
        _branchPhotoRepository = branchPhotoRepository;
    }

    public async Task<BranchPhotoResponse> AddPhotoForBooth(Guid branchID, IFormFile file)
    {
        var booth = GetByIdAsync(branchID);
        if (booth != null)
        {
            //upload to cloudinary
            var uploadResult = await _cloudinaryService.AddPhotoAsync(file, "FBooth-BranchPicture");
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            //create object from cloudinary's return 
            var Photo = new BranchPhoto
            {
                BranchID = branchID,
                BranchPhotoUrl = uploadResult.SecureUrl.AbsoluteUri,
                CouldID = uploadResult.PublicId,
            };

            await _branchPhotoRepository.AddAsync(Photo);

            var updatedBranch = (await _branchRepository.GetAsync(b => b.BranchID == branchID, b => b.BranchPhotos)).FirstOrDefault();
            return _mapper.Map<BranchPhotoResponse>(updatedBranch);
        }
        throw new KeyNotFoundException("Branch not found.");
    }

    //Create
    public async Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel, BranchStatus status)
    {
            //validate input
            //if (createModel.ManagerID.HasValue)
            //{
            //    await ValideManagerForBranch(createModel.ManagerID.Value);
            //}
            if (createModel.ClosingTime < createModel.OpeningTime)
            {
                throw new BadRequestException("Closing time must after the opening time");
            }
            Branch branch = _mapper.Map<Branch>(createModel);
            branch.Status = status;
            branch.CreateDate = DateTimeHelper.GetVietnamTimeNow();
            await _branchRepository.AddAsync(branch);
            return _mapper.Map<CreateBranchResponse>(branch);
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
        var branches = await _branchRepository.GetAsync(null, bth => bth.Booths, bth => bth.BranchPhotos);
        return _mapper.Map<IEnumerable<BranchResponse>>(branches.ToList().OrderByDescending(i=>i.CreateDate));
    }

    public async Task<IEnumerable<BranchResponse>> GetAllPagingAsync(BranchFilter filter, PagingModel paging)
    {
        var branches = (await _branchRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listBranchresponse = _mapper.Map<IEnumerable<BranchResponse>>(branches);
        return listBranchresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.CreateDate);
    }

    public async Task<BranchResponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _branchRepository.GetAsync(p => p.BranchID == id, p => p.BranchPhotos)).FirstOrDefault();
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
        //if (updateModel.ManagerID.HasValue)
        //{
        //    await ValideManagerForBranch(updateModel.ManagerID.Value);
        //}
        if (updateBranch.ClosingTime < updateBranch.OpeningTime)
        {
            throw new BadRequestException("Closing time must after the opening time");
        }
        if (status.HasValue)
        {
            updateBranch.Status = status.Value;
        }
        await _branchRepository.UpdateAsync(updateBranch);
    }

}

