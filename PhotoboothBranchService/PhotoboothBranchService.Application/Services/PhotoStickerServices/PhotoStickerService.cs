using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoStickerServices
{
    public class PhotoStickerService : IPhotoStickerService
    {
        private readonly IPhotoStickerRepository _mapStickerRepository;
        private readonly IMapper _mapper;

        public PhotoStickerService(IPhotoStickerRepository mapStickerRepository, IMapper mapper)
        {
            _mapStickerRepository = mapStickerRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<CreatePhotoStickerResponse> CreateAsync(CreatePhotoStickerRequest createModel)
        {
            var mapSticker = _mapper.Map<PhotoSticker>(createModel);
            await _mapStickerRepository.AddAsync(mapSticker);
            return _mapper.Map<CreatePhotoStickerResponse>(mapSticker);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var mapStickers = await _mapStickerRepository.GetAsync(m => m.PhotoStickerID == id);
                var mapSticker = mapStickers.FirstOrDefault();
                if (mapSticker != null)
                {
                    await _mapStickerRepository.RemoveAsync(mapSticker);
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
            var mapStickers = await _mapStickerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoStickerResponse>>(mapStickers.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<PhotoStickerResponse>> GetAllPagingAsync(PhotoStickerFilter filter, PagingModel paging)
        {
            var mapStickers = (await _mapStickerRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listMapStickerResponse = _mapper.Map<IEnumerable<PhotoStickerResponse>>(mapStickers);
            return listMapStickerResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<PhotoStickerResponse> GetByIdAsync(Guid id)
        {
            var mapStickers = await _mapStickerRepository.GetAsync(m => m.PhotoStickerID == id);
            return _mapper.Map<PhotoStickerResponse>(mapStickers.FirstOrDefault());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdatePhotoStickerRequest updateModel)
        {
            var mapSticker = (await _mapStickerRepository.GetAsync(m => m.PhotoStickerID == id)).FirstOrDefault();
            if (mapSticker == null)
            {
                throw new KeyNotFoundException("Map Sticker not found.");
            }

            var updateMapSticker = _mapper.Map(updateModel, mapSticker);
            await _mapStickerRepository.UpdateAsync(updateMapSticker);
        }
    }
}
