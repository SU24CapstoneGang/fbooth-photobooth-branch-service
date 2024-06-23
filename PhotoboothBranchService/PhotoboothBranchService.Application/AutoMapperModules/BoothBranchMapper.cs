using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BoothBranchMapper : Profile
    {
        public BoothBranchMapper()
        {
            CreateMap<CreateBoothBranchRequest, BoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBoothBranchRequest, BoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<BoothBranch, BoothBranchResponse>()
                //.ForMember(des => des.AccountName, opt => opt.MapFrom<FullNameResolver>())
                .ReverseMap()
                .HandleNullProperty();
            CreateMap<BoothBranch, CreateBoothBranchResponse>().HandleNullProperty();
        }
    }

    //public class FullNameResolver : IValueResolver<PhotoBoothBranch, PhotoBoothBranchresponse, string>
    //{
    //    public string Resolve(PhotoBoothBranch source, PhotoBoothBranchresponse destination, string destMember, ResolutionContext context)
    //    {
    //        return $"{source.Account.FirstName} {source.Account.LastName}";
    //    }
    //}
}
