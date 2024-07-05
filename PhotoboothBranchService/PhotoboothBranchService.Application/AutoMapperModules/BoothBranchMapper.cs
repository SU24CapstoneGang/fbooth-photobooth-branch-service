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
            CreateMap<CreateBranchRequest, BoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBranchRequest, BoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<BoothBranch, BranchResponse>()
                //.ForMember(des => des.AccountName, opt => opt.MapFrom<FullNameResolver>())
                .ReverseMap()
                .HandleNullProperty();
            CreateMap<BoothBranch, CreateBranchResponse>().HandleNullProperty();
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
