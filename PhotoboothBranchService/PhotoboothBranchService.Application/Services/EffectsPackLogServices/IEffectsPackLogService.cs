using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.EffectsPackLog;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.EffectsPackLogServices
{
    public interface IEffectsPackLogService : IService<EffectsPackLogResponse,
        CreateEffectsPackLogRequest,
        UpdateEffectsPackLogRequest,
        EffectsPackLogFilter,
        PagingModel>
    {
    }
}
