using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public interface IBoothService : IService<BoothResponse, CreateBoothRequest, UpdateBoothRequest, BoothFilter, PagingModel>
    {
    }
}
