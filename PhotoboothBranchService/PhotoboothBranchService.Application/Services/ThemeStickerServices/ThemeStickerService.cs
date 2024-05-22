using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ThemeStickerServices
{
    public class ThemeStickerService : IThemeStickerService
    {
        private readonly IThemeStickerRepository _themeStickerRepository;
        private readonly IMapper _mapper;

        public ThemeStickerService(IThemeStickerRepository themeStickerRepository, IMapper mapper)
        {
            _themeStickerRepository = themeStickerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateThemeStickerRequest createModel)
        {
            ThemeSticker themeSticker = _mapper.Map<ThemeSticker>(createModel);
            return await _themeStickerRepository.AddAsync(themeSticker);
        }

        public async Task DeleteAsync(Guid id)
        {
            var themeSticker = await _themeStickerRepository.GetAsync(t => t.ThemeStickerID == id);
            var themeStickerToDelete = themeSticker.FirstOrDefault();
            if (themeStickerToDelete != null)
            {
                await _themeStickerRepository.RemoveAsync(themeStickerToDelete);
            }
        }

        public async Task<IEnumerable<ThemeStickerResponse>> GetAllAsync()
        {
            var themeStickers = await _themeStickerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ThemeStickerResponse>>(themeStickers.ToList());
        }

        public async Task<IEnumerable<ThemeStickerResponse>> GetAllPagingAsync(ThemeStickerFilter filter, PagingModel paging)
        {
            var themeStickers = (await _themeStickerRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var themeStickersResponse = _mapper.Map<IEnumerable<ThemeStickerResponse>>(themeStickers);
            themeStickersResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return themeStickersResponse;
        }

        public async Task<ThemeStickerResponse> GetByIdAsync(Guid id)
        {
            var themeStickers = await _themeStickerRepository.GetAsync(t => t.ThemeStickerID == id);
            return _mapper.Map<ThemeStickerResponse>(themeStickers);
        }

        public async Task<IEnumerable<ThemeStickerResponse>> GetByName(string name)
        {
            var themeStickers = await _themeStickerRepository.GetAsync(i=>i.ThemeStickerName.Contains(name));
            return _mapper.Map<IEnumerable<ThemeStickerResponse>>(themeStickers.ToList());
        }

        public async Task UpdateAsync(Guid id, UpdateThemeStickerRequest updateModel)
        {
            var themeSticker = (await _themeStickerRepository.GetAsync(t => t.ThemeStickerID == id)).FirstOrDefault();
            if (themeSticker == null)
            {
                throw new KeyNotFoundException("Theme sticker not found.");
            }

            var updatedThemeSticker = _mapper.Map(updateModel, themeSticker);
            await _themeStickerRepository.UpdateAsync(updatedThemeSticker);
        }
    }
}
