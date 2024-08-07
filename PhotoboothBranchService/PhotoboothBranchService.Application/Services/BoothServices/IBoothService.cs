using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public interface IBoothService : IServiceBase<BoothResponse, BoothFilter, PagingModel>
    {
        Task<IEnumerable<BoothResponse>> GetByName(string name);
        Task<CreateBoothResponse> CreateAsync(CreateBoothRequest createModel, BoothStatus status);
        Task<IEnumerable<BoothResponse>> StaffGetAllAsync(string? email);
        Task<IEnumerable<BoothResponse>> GetAvtiveBoothByTime(GetAvtiveBoothByTimeRequest request);
        Task UpdateAsync(Guid id, UpdateBoothRequest updateModel, BoothStatus? status);
        Task DeleteAsync(Guid id);
    }
}
