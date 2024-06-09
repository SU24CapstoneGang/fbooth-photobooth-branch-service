using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.MapStickerServices
{
    public class MapStickerService : IMapStickerService
    {
        private readonly IMapStickerRepository _mapStickerRepository;
        private readonly IMapper _mapper;

        public MapStickerService(IMapStickerRepository mapStickerRepository, IMapper mapper)
        {
            _mapStickerRepository = mapStickerRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<Guid> CreateAsync(CreateMapStickerRequest createModel)
        {
            var mapSticker = _mapper.Map<PhotoSticker>(createModel);
            return await _mapStickerRepository.AddAsync(mapSticker);
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
        public async Task<IEnumerable<MapStickerResponse>> GetAllAsync()
        {
            var mapStickers = await _mapStickerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MapStickerResponse>>(mapStickers.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<MapStickerResponse>> GetAllPagingAsync(MapStickerFilter filter, PagingModel paging)
        {
            var mapStickers = (await _mapStickerRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listMapStickerResponse = _mapper.Map<IEnumerable<MapStickerResponse>>(mapStickers);
            listMapStickerResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listMapStickerResponse;
        }

        // Read by ID
        public async Task<MapStickerResponse> GetByIdAsync(Guid id)
        {
            var mapStickers = await _mapStickerRepository.GetAsync(m => m.PhotoStickerID == id);
            return _mapper.Map<MapStickerResponse>(mapStickers);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateMapStickerRequest updateModel)
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
