using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class AccountAutoMapper : Profile
    {
        public AccountAutoMapper()
        {
            CreateMap<Account, AccountRegisterResponse>().ReverseMap().HandleNullProperty();
            CreateMap<Account, AccountResponse>().ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName)).ReverseMap();
            CreateMap<CreateAccountRequestModel, Account>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.AccountID, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.RoleID, opt => opt.Ignore()).ReverseMap().HandleNullProperty();

            CreateMap<UpdateAccountRequestModel, Account>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.AccountID, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore()) 
            .ForMember(dest => dest.RoleID, opt => opt.Ignore()) 
            .ForMember(dest => dest.RoleID, opt => opt.Ignore()).ReverseMap().HandleNullProperty();
        }
    }
}
