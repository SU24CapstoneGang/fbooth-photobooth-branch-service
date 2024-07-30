using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenCvSharp;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.PhotoBoxServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public class LayoutService : ILayoutService
{
    private readonly ILayoutRepository _layoutRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IPhotoBoxRepository _photoBoxRepository;

    public LayoutService(ILayoutRepository layoutRepository, IMapper mapper, ICloudinaryService cloudinaryService, IPhotoBoxRepository photoBoxRepository)
    {
        _layoutRepository = layoutRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _photoBoxRepository = photoBoxRepository;
    }


    //[HttpPost("add-layout-auto")]
    public async Task<LayoutResponse> CreateLayoutAuto(IFormFile file)
    {
        var layout = await this.DefineLayoutDetail(file);
            layout.Status = StatusUse.Available;

        // Upload lên Cloudinary
        var uploadResult = await _cloudinaryService.AddPhotoAsync(file, "FBooth-Layout");
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        layout.LayoutURL = uploadResult.SecureUrl.AbsoluteUri;
        layout.CouldID = uploadResult.PublicId;

        await _layoutRepository.AddAsync(layout);
        return _mapper.Map<LayoutResponse>(layout);
    }

    private async Task<Layout> DefineLayoutDetail(IFormFile file)
    {
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);

            using (var srcImage = OpenCvSharp.Mat.FromStream(ms, ImreadModes.Unchanged))
            {
                Mat[] channels = Cv2.Split(srcImage);
                if (channels.Length < 4)
                {
                    throw new Exception("Hình ảnh không có kênh alpha.");
                }

                //(kênh trong suốt)
                Mat alphaChannel = channels[3];
                Mat binaryImage = new Mat();
                Cv2.Threshold(alphaChannel, binaryImage, 0, 255, ThresholdTypes.BinaryInv);

                // Tìm các đường viền của các vùng trong suốt
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // Lưu trữ các hình chữ nhật bao quanh
                List<Rect> boundingRects = new List<Rect>();
                foreach (var contour in contours)
                {
                    var rect = Cv2.BoundingRect(contour);
                    if (rect.Width > 50 && rect.Height > 50)
                    {
                        boundingRects.Add(rect);
                    }
                }

                // Kiểm tra nếu không có ô trống thì không phải là layout
                if (boundingRects.Count == 0)
                {
                    throw new Exception("Hình ảnh không phải là layout hợp lệ (không có ô trống).");
                }

                var layout = new Layout
                {
                    LayoutCode = Path.GetFileNameWithoutExtension(file.FileName),
                    PhotoBoxes = new List<PhotoBox>()
                };

                // Sắp xếp các hình chữ nhật bao quanh theo tọa độ Y (từ trên xuống dưới) và sau đó theo tọa độ X (từ trái sang phải)
                boundingRects.Sort((r1, r2) =>
                {
                    int result = r1.Y.CompareTo(r2.Y);
                    return result == 0 ? r1.X.CompareTo(r2.X) : result;
                });

                // Tạo các đối tượng PhotoBox và thiết lập các thuộc tính
                short boxCount = 0;
                foreach (var rect in boundingRects)
                {
                    boxCount++;
                    var photoBox = new PhotoBox
                    {
                        BoxHeight = rect.Height,
                        BoxWidth = rect.Width,
                        CoordinatesX = rect.X,
                        CoordinatesY = rect.Y,
                        LayoutID = layout.LayoutID
                    };

                    // Thiết lập IsLandscape và BoxIndex
                    photoBox.IsLandscape = rect.Width > rect.Height;
                    photoBox.BoxIndex = boxCount;

                    layout.PhotoBoxes.Add(photoBox);
                }

                // Thiết lập kích thước layout và số lượng ô ảnh
                layout.Height = srcImage.Height;
                layout.Width = srcImage.Width;
                layout.PhotoSlot = boxCount;

                return layout;
            }
        }
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
                await _cloudinaryService.DeletePhotoAsync(layout.CouldID);
            }
            else
            {
                throw new NotFoundException($"Not found layout id {id}");
            }
        }
        catch
        {
            throw new BadRequestException("Fail for deleting layout"); ;
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
        return listLayoutresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    public async Task<LayoutResponse> GetByIdAsync(Guid id)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == id)).FirstOrDefault();
        return _mapper.Map<LayoutResponse>(layout);
    }

    public async Task UpdateLayoutAsync(IFormFile? file, Guid LayoutID, UpdateLayoutRequest updateLayoutRequest)
    {
        var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == LayoutID, l => l.PhotoBoxes)).FirstOrDefault();
        if (layout == null)
        {
            throw new KeyNotFoundException("Layout not found.");
        }
        var updateLayout = _mapper.Map(updateLayoutRequest, layout);
        if (file != null)
        {
            var templayout = await this.DefineLayoutDetail(file);
            foreach (var box in layout.PhotoBoxes)
            {
                await _photoBoxRepository.RemoveAsync(box);
            }
            foreach (var box in templayout.PhotoBoxes)
            {
                box.LayoutID = LayoutID;
                await _photoBoxRepository.AddAsync(box);
            }
            updateLayout.LayoutCode = templayout.LayoutCode;
            updateLayout.Height = templayout.Height;
            updateLayout.Width = templayout.Width;
            updateLayout.PhotoSlot = templayout.PhotoSlot;

        }
        await _layoutRepository.UpdateAsync(updateLayout);
        await _cloudinaryService.UpdatePhotoAsync(file, layout.CouldID);
    }
}

