using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public interface IBoothService : IServiceBase<BoothResponse, BoothFilter, PagingModel>
    {
        Task<IEnumerable<BoothResponse>> GetByName(string name);
        public Task<CreateBoothResponse> CreateAsync(CreateBoothRequest createModel);
        public Task UpdateAsync(Guid id, UpdateBoothRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
