using AutoMapper;
using CloudinaryDotNet.Core;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public class LayoutService : ILayoutService
{
    private readonly ILayoutRepository _layoutRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public LayoutService(ILayoutRepository layoutRepository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _layoutRepository = layoutRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    // Create
    public async Task<Guid> CreateAsync(CreateLayoutRequest createModel)
    {
        Layout layout = _mapper.Map<Layout>(createModel);
        return await _layoutRepository.AddAsync(layout);
    }
    public async Task<LayoutResponse> CreateLayoutAsync(IFormFile file, CreateLayoutRequest createModel)
    {

        //upload to cloudinary
        var uploadResult = await _cloudinaryService.AddPhotoAsync(file);
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        //create object from cloudinary's return 
        var layout = _mapper.Map<Layout>(createModel);

        layout.LayoutURL = uploadResult.SecureUrl.AbsoluteUri;
        layout.CouldID = uploadResult.PublicId;
        layout.Status = StatusUse.Available;

        await _layoutRepository.AddAsync(layout);

        return _mapper.Map<LayoutResponse>(layout);
    }

    // Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Layout? layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
            if (layout != null)
            {
                await _layoutRepository.RemoveAsync(layout);
            } else
            {
                throw new NotFoundException($"Not found kayout id {id}");
            }
        }
        catch
        {
            throw;
        }
    }

    // Read
    public async Task<IEnumerable<LayoutResponse>> GetAllAsync()
    {
        var layouts = await _layoutRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LayoutResponse>>(layouts.ToList());
    }

    public async Task<IEnumerable<LayoutResponse>> GetAllPagingAsync(LayoutFilter filter, PagingModel paging)
    {
        var layouts = (await _layoutRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listLayoutresponse = _mapper.Map<IEnumerable<LayoutResponse>>(layouts);
        listLayoutresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listLayoutresponse;
    }

    public async Task<LayoutResponse> GetByIdAsync(Guid id)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
        return _mapper.Map<LayoutResponse>(layout);
    }

    // Update
    public async Task UpdateAsync(Guid id, UpdateLayoutRequest updateMdel)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
        if (layout == null)
        {
            throw new KeyNotFoundException("Layout not found.");
        }

        var updateLayout = _mapper.Map(updateMdel, layout);
        updateLayout.LastModified = DateTime.UtcNow;
        await _layoutRepository.UpdateAsync(updateLayout);
    }
}

