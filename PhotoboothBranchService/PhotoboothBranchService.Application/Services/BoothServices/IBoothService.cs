using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public interface IBoothService : IServiceBase<BoothResponse, BoothFilter, PagingModel>
    {
        Task<IEnumerable<BoothResponse>> GetByName(string name);
        Task<CreateBoothResponse> CreateAsync(CreateBoothRequest createModel);
        Task<BoothResponse> AddPhotoForBooth(Guid boothID, IFormFile file);
        Task<IEnumerable<BoothResponse>> StaffGetAllAsync(string? email);
        Task<IEnumerable<BoothResponse>> CustomerGetAllAsync();
        Task UpdateAsync(Guid id, UpdateBoothRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
