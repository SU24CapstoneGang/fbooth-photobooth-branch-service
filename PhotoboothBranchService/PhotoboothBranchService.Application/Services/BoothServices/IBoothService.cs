using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public interface IBoothService : IServiceBase<BoothResponse, CreateBoothRequest, CreateBoothResponse, UpdateBoothRequest, BoothFilter, PagingModel>
    {
        Task<IEnumerable<BoothResponse>> GetByName(string name);
    }
}
