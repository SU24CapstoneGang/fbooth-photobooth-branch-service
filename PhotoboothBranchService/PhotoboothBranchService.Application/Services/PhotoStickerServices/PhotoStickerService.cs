using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoStickerServices
{
    public class PhotoStickerService : IPhotoStickerService
    {
        private readonly IPhotoStickerRepository _photoStickerRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IStickerRepository _stickerRepository;
        private readonly IMapper _mapper;

        public PhotoStickerService(IPhotoStickerRepository mapStickerRepository, IMapper mapper, IStickerRepository stickerRepository, IPhotoRepository photoRepository)
        {
            _photoStickerRepository = mapStickerRepository;
            _mapper = mapper;
            _stickerRepository = stickerRepository;
            _photoRepository = photoRepository;
        }

        // Create
        public async Task<CreatePhotoStickerResponse> CreateAsync(CreatePhotoStickerRequest createModel)
        {
            var mapSticker = _mapper.Map<PhotoSticker>(createModel);

            var photo = (await _photoRepository.GetAsync(i => i.PhotoID == createModel.PhotoID)).SingleOrDefault();
            if (photo == null)
            {
                throw new NotFoundException("Not found photo to add");
            }
            var sticker = (await _stickerRepository.GetAsync(i => i.StickerID == createModel.StickerID)).SingleOrDefault();
            if (sticker == null)
            {
                throw new NotFoundException("Not sticker photo to add");
            }
            await _photoStickerRepository.AddAsync(mapSticker);
            return _mapper.Map<CreatePhotoStickerResponse>(mapSticker);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var mapStickers = await _photoStickerRepository.GetAsync(m => m.PhotoStickerID == id);
                var mapSticker = mapStickers.FirstOrDefault();
                if (mapSticker != null)
                {
                    await _photoStickerRepository.RemoveAsync(mapSticker);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<PhotoStickerResponse>> GetAllAsync()
        {
            var mapStickers = await _photoStickerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoStickerResponse>>(mapStickers.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<PhotoStickerResponse>> GetAllPagingAsync(PhotoStickerFilter filter, PagingModel paging)
        {
            var mapStickers = (await _photoStickerRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listMapStickerResponse = _mapper.Map<IEnumerable<PhotoStickerResponse>>(mapStickers);
            return listMapStickerResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<PhotoStickerResponse> GetByIdAsync(Guid id)
        {
            var mapStickers = await _photoStickerRepository.GetAsync(m => m.PhotoStickerID == id);
            return _mapper.Map<PhotoStickerResponse>(mapStickers.FirstOrDefault());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdatePhotoStickerRequest updateModel)
        {
            var mapSticker = (await _photoStickerRepository.GetAsync(m => m.PhotoStickerID == id)).FirstOrDefault();
            if (mapSticker == null)
            {
                throw new KeyNotFoundException("Map Sticker not found.");
            }

            var updateMapSticker = _mapper.Map(updateModel, mapSticker);
            await _photoStickerRepository.UpdateAsync(updateMapSticker);
        }
    }
}
