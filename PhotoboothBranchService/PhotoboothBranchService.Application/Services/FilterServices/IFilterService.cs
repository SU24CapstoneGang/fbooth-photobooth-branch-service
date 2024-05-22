using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Filter;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FilterServices;

public interface IFilterService : IService<Filterresponse, CreateFilterRequest, UpdateFilterRequest, FilterFilter, PagingModel>
{
    Task<IEnumerable<Filterresponse>> GetByName(string name);
}
