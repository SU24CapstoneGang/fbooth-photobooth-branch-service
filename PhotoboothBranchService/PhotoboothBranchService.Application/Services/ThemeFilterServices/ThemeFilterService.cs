using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ThemeFilterServices
{
    public class ThemeFilterService : IThemeFilterService
    {
        private readonly IThemeFilterRepository _themeFilterRepository;
        private readonly IMapper _mapper;

        public ThemeFilterService(IThemeFilterRepository themeFilterRepository, IMapper mapper)
        {
            _themeFilterRepository = themeFilterRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateThemeFilterRequest createModel)
        {
            ThemeFilter themeFilter = _mapper.Map<ThemeFilter>(createModel);
            return await _themeFilterRepository.AddAsync(themeFilter);
        }

        public async Task DeleteAsync(Guid id)
        {
            var themeFilter = await _themeFilterRepository.GetAsync(t => t.ThemeFilterID == id);
            var themeFilterToDelete = themeFilter.FirstOrDefault();
            if (themeFilterToDelete != null)
            {
                await _themeFilterRepository.RemoveAsync(themeFilterToDelete);
            }
        }

        public async Task<IEnumerable<ThemeFilterResponse>> GetAllAsync()
        {
            var themeFilters = await _themeFilterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ThemeFilterResponse>>(themeFilters.ToList());
        }

        public async Task<IEnumerable<ThemeFilterResponse>> GetAllPagingAsync(ThemeFilterFilter filter, PagingModel paging)
        {
            var themeFilters = (await _themeFilterRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var themeFiltersResponse = _mapper.Map<IEnumerable<ThemeFilterResponse>>(themeFilters);
            themeFiltersResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return themeFiltersResponse;
        }

        public async Task<ThemeFilterResponse> GetByIdAsync(Guid id)
        {
            var themeFilters = await _themeFilterRepository.GetAsync(t => t.ThemeFilterID == id);
            return _mapper.Map<ThemeFilterResponse>(themeFilters);
        }

        public async Task<IEnumerable<ThemeFilterResponse>> GetByName(string name)
        {
            var themeFilters = await _themeFilterRepository.GetAsync(i=>i.ThemeFilterName.Contains(name));
            return _mapper.Map<IEnumerable<ThemeFilterResponse>>(themeFilters.ToList());
        }

        public async Task UpdateAsync(Guid id, UpdateThemeFilterRequest updateModel)
        {
            var themeFilter = (await _themeFilterRepository.GetAsync(t => t.ThemeFilterID == id)).FirstOrDefault();
            if (themeFilter == null)
            {
                throw new KeyNotFoundException("Theme filter not found.");
            }

            var updatedThemeFilter = _mapper.Map(updateModel, themeFilter);
            await _themeFilterRepository.UpdateAsync(updatedThemeFilter);
        }
    }
}
