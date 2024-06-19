using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Theme;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.ThemeServices
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeFrameRepository;
        private readonly IMapper _mapper;

        public ThemeService(IThemeRepository themeFrameRepository, IMapper mapper)
        {
            _themeFrameRepository = themeFrameRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateThemeRequest createModel)
        {
            Theme themeFrame = _mapper.Map<Theme>(createModel);
            return await _themeFrameRepository.AddAsync(themeFrame);
        }

        public async Task DeleteAsync(Guid id)
        {
            var themeFrame = await _themeFrameRepository.GetAsync(t => t.ThemeID == id);
            var themeFrameToDelete = themeFrame.FirstOrDefault();
            if (themeFrameToDelete != null)
            {
                await _themeFrameRepository.RemoveAsync(themeFrameToDelete);
            }
        }

        public async Task<IEnumerable<ThemeResponse>> GetAllAsync()
        {
            var themeFrames = await _themeFrameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ThemeResponse>>(themeFrames.ToList());
        }

        public async Task<IEnumerable<ThemeResponse>> GetAllPagingAsync(ThemeFilter filter, PagingModel paging)
        {
            var themeFrames = (await _themeFrameRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var themeFramesResponse = _mapper.Map<IEnumerable<ThemeResponse>>(themeFrames);
            themeFramesResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return themeFramesResponse;
        }

        public async Task<ThemeResponse> GetByIdAsync(Guid id)
        {
            var themeFrames = await _themeFrameRepository.GetAsync(t => t.ThemeID == id);
            return _mapper.Map<ThemeResponse>(themeFrames);
        }

        public async Task<IEnumerable<ThemeResponse>> GetByName(string name)
        {
            var themeFrames = await _themeFrameRepository.GetAsync(i => i.ThemeName.Contains(name));
            return _mapper.Map<IEnumerable<ThemeResponse>>(themeFrames.ToList());
        }

        public async Task UpdateAsync(Guid id, UpdateThemeRequest updateModel)
        {
            var themeFrame = (await _themeFrameRepository.GetAsync(t => t.ThemeID == id)).FirstOrDefault();
            if (themeFrame == null)
            {
                throw new KeyNotFoundException("Theme frame not found.");
            }

            var updatedThemeFrame = _mapper.Map(updateModel, themeFrame);
            await _themeFrameRepository.UpdateAsync(updatedThemeFrame);
        }
    }
}

