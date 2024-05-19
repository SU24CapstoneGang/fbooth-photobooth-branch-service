using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Filter;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Filter;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FilterServices;

public interface IFilterService : IService<Filterresponse,CreateFilterRequest,UpdateFilterRequest,FilterFilter,PagingModel>
{
    Task<IEnumerable<Filterresponse>> GetByName(string name);
}
