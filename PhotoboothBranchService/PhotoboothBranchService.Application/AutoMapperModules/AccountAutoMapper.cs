using AutoMapper;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class AccountAutoMapper : Profile
    {
        public AccountAutoMapper()
        {
            CreateMap<Account, AccountRegisterResponse>().ReverseMap();
            CreateMap<Account, AccountRespone>().ReverseMap();
            CreateMap<CreateAccountRequestModel, Account>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.AccountID, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.RoleID, opt => opt.Ignore()).ReverseMap();
        }
    }
}
