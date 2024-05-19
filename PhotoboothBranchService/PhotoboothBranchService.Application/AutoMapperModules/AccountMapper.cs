using AutoMapper;
using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, CreateAccountRequestModel>().ReverseMap();
        }
    }
}
