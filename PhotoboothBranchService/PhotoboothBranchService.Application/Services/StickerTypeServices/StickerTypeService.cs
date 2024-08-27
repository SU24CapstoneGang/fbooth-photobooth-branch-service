using AutoMapper;
using CloudinaryDotNet;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.StickerType;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.StickerTypeServices
{
    public class StickerTypeService : IStickerTypeService
    {
        private readonly IStickerTypeRepository _stickerTypeRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public StickerTypeService(IStickerTypeRepository stickerTypeRepository, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _stickerTypeRepository = stickerTypeRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StickerTypeResponse>> GetAllAsync()
        {
            var stickerTypes = (await _stickerTypeRepository.GetAsync(null, s => s.Stickers)).ToList();
            return _mapper.Map<IEnumerable<StickerTypeResponse>>(stickerTypes).OrderByDescending(i => i.CreatedDate);
        }
        public async Task<IEnumerable<StickerTypeResponse>> GetAllAvailbleAsync()
        {
            var stickerTypes = (await _stickerTypeRepository.GetAsync(null, s => s.Stickers)).ToList();
           /* Parallel.ForEachAsync(stickerTypes, )*/
            return _mapper.Map<IEnumerable<StickerTypeResponse>>(stickerTypes).OrderByDescending(i => i.CreatedDate);
        }
        public async Task<IEnumerable<StickerTypeResponse>> GetAllPagingAsync(StickerTypeFilter filter, PagingModel paging)
        {
            var stickerTypes = (await _stickerTypeRepository.GetAsync(null, s => s.Stickers)).ToList().AutoFilter(filter);
            var listStickerTypeResponse = _mapper.Map<IEnumerable<StickerTypeResponse>>(stickerTypes);
            return listStickerTypeResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.CreatedDate);
        }

        public async Task<StickerTypeResponse> GetByIdAsync(Guid id)
        {
            var stickerTypes = await _stickerTypeRepository.GetAsync(s => s.StickerTypeID == id, s => s.Stickers);
            var stickerType = stickerTypes.FirstOrDefault();
            if (stickerType == null)
            {
                throw new NotFoundException("Service not found");
            }
            return _mapper.Map<StickerTypeResponse>(stickerType);
        }

        public async Task<StickerTypeResponse> CreateAsync(CreateStickerTypeRequest createModel)
        {
            var stickerType = _mapper.Map<StickerType>(createModel);

            //upload to cloudinary
            var uploadResult = await _cloudinaryService.AddPhotoAsync(createModel.File, "FBooth-Sticker-Type");
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }
            stickerType.CouldID = uploadResult.PublicId;
            stickerType.RepresentImageURL = uploadResult.SecureUrl.AbsoluteUri;
            await _stickerTypeRepository.AddAsync(stickerType);
            return _mapper.Map<StickerTypeResponse>(stickerType);
        }

        public async Task UpdateAsync(Guid id, UpdateStickerTypeRequest updateModel)
        {
            var stickerType = (await _stickerTypeRepository.GetAsync(s => s.StickerTypeID == id)).FirstOrDefault();
            if (stickerType == null)
            {
                throw new KeyNotFoundException("Sticker type not found.");
            }
            if (updateModel.File != null && updateModel.File.Length != 0)
            {
                var result = await _cloudinaryService.UpdatePhotoAsync(updateModel.File, stickerType.CouldID);
                stickerType.RepresentImageURL = result.SecureUrl.AbsoluteUri;
            }
            var updatedStickerType = _mapper.Map(updateModel, stickerType);
            await _stickerTypeRepository.UpdateAsync(updatedStickerType);
        }
    }
}
