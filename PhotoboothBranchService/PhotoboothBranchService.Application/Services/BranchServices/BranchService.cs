using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using OpenCvSharp;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace PhotoboothBranchService.Application.Services.BranchServices;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IBranchPhotoRepository _branchPhotoRepository;
    private readonly IBoothPhotoRepository _boothPhotoRepository;
    private readonly IMapper _mapper;

    public BranchService(IBranchRepository branchRepository, IMapper mapper, IAccountRepository accountRepository, 
        ICloudinaryService cloudinaryService, IBranchPhotoRepository branchPhotoRepository, IBoothPhotoRepository boothPhotoRepository)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _cloudinaryService = cloudinaryService;
        _branchPhotoRepository = branchPhotoRepository;
        _boothPhotoRepository = boothPhotoRepository;
    }

    public async Task<BranchResponse> AddPhotoForBranch(Guid branchID, IFormFile file)
    {
        var branch = await GetByIdAsync(branchID);
        if (branch != null)
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
            return _mapper.Map<BranchResponse>(updatedBranch);
        }
        throw new KeyNotFoundException("Branch not found.");
    }

    //Create
    public async Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel)
    {
        Branch branch = _mapper.Map<Branch>(createModel);
        branch.OpeningTime = new TimeSpan(7, 0, 0);
        branch.ClosingTime = new TimeSpan(23, 0, 0);
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
        return _mapper.Map<IEnumerable<BranchResponse>>(branches.ToList().OrderByDescending(i=>i.CreatedDate));
    }

    public async Task<IEnumerable<BranchResponse>> GetAllPagingAsync(BranchFilter filter, PagingModel paging)
    {
        var branches = (await _branchRepository.GetAsync(null, bth => bth.Booths, bth => bth.BranchPhotos)).ToList().AutoFilter(filter);
        var listBranchresponse = _mapper.Map<IEnumerable<BranchResponse>>(branches);
        return listBranchresponse.AsQueryable().OrderByDescending(i => i.CreatedDate).AutoPaging(paging.PageSize, paging.PageIndex);
    }
    public async Task<IEnumerable<BranchResponse>> GetAvailbleAsync()
    {
        var branches = (await _branchRepository.GetAsync(i => i.Status == BranchStatus.Active, includeProperties: new Expression<Func<Branch, object>>[]
            {
                u => u.Booths,
                u => u.BranchPhotos
            })).ToList();
        await Parallel.ForEachAsync(branches, (branch, cancellationToken) =>
        {
            branch.Booths = branch.Booths.Where(b => b.Status != BoothStatus.Inactive).ToList();
            return ValueTask.CompletedTask;
        });
        foreach (var branch in branches)
        {
            foreach (var booth in branch.Booths)
            {
                booth.BoothPhotos = (await _boothPhotoRepository.GetAsync(i => i.BoothID == booth.BoothID)).ToList();
            }
        }
        return _mapper.Map<IEnumerable<BranchResponse>>(branches.ToList().OrderByDescending(i => i.LastModified));
    }
    public async Task<BranchResponse> GetByIdAsync(Guid id)
    {
        var photoBoothBranch = (await _branchRepository.GetAsync(p => p.BranchID == id, p => p.BranchPhotos, p => p.Booths)).FirstOrDefault();
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
    public async Task UpdateAsync(Guid id, UpdateBranchRequest updateModel)
    {
        var branch = (await _branchRepository.GetAsync(p => p.BranchID == id)).FirstOrDefault();
        if (branch == null)
        {
            throw new NotFoundException("Branch not found.");
        }

        var updateBranch = _mapper.Map(updateModel, branch);
        await _branchRepository.UpdateAsync(updateBranch);
    }

}

