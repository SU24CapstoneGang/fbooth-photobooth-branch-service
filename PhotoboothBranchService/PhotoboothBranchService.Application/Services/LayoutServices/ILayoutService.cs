using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public interface ILayoutService : IService<Layoutresponse, CreateLayoutRequest, UpdateLayoutRequest, LayoutFilter, PagingModel>
{
}
