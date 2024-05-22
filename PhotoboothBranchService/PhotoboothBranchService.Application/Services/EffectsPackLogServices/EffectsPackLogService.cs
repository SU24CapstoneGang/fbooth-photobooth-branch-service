using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.EffectsPackLog;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.EffectsPackLogServices
{
    public class EffectsPackLogService : IEffectsPackLogService
    {
        private readonly IEffectsPackLogRepository _effectsPackLogRepository;
        private readonly IMapper _mapper;

        public EffectsPackLogService(IEffectsPackLogRepository effectsPackLogRepository, IMapper mapper)
        {
            _effectsPackLogRepository = effectsPackLogRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateEffectsPackLogRequest createModel)
        {
            EffectsPackLog effectsPackLog = _mapper.Map<EffectsPackLog>(createModel);
            return await _effectsPackLogRepository.AddAsync(effectsPackLog);
        }

        public async Task DeleteAsync(Guid id)
        {
            var effectsPackLog = await _effectsPackLogRepository.GetAsync(e => e.PacklogID == id);
            var effectsPackLogToDelete = effectsPackLog.FirstOrDefault();
            if (effectsPackLogToDelete != null)
            {
                await _effectsPackLogRepository.RemoveAsync(effectsPackLogToDelete);
            }
        }

        public async Task<IEnumerable<EffectsPackLogResponse>> GetAllAsync()
        {
            var effectsPackLogs = await _effectsPackLogRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EffectsPackLogResponse>>(effectsPackLogs.ToList());
        }

        public async Task<IEnumerable<EffectsPackLogResponse>> GetAllPagingAsync(EffectsPackLogFilter filter, PagingModel paging)
        {
            var effectsPackLogs = (await _effectsPackLogRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var effectsPackLogsResponse = _mapper.Map<IEnumerable<EffectsPackLogResponse>>(effectsPackLogs);
            effectsPackLogsResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return effectsPackLogsResponse;
        }

        public async Task<EffectsPackLogResponse> GetByIdAsync(Guid id)
        {
            var effectsPackLogs = await _effectsPackLogRepository.GetAsync(e => e.PacklogID == id);
            return _mapper.Map<EffectsPackLogResponse>(effectsPackLogs);
        }

        public async Task UpdateAsync(Guid id, UpdateEffectsPackLogRequest updateModel)
        {
            var effectsPackLog = (await _effectsPackLogRepository.GetAsync(e => e.PacklogID == id)).FirstOrDefault();
            if (effectsPackLog == null)
            {
                throw new KeyNotFoundException("Effects pack log not found.");
            }

            var updatedEffectsPackLog = _mapper.Map(updateModel, effectsPackLog);
            await _effectsPackLogRepository.UpdateAsync(updatedEffectsPackLog);
        }
    }
}
