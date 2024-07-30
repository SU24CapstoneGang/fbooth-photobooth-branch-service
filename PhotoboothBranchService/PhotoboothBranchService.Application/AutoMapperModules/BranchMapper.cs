using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BranchMapper : Profile
    {
        public BranchMapper()
        {
            CreateMap<CreateBranchRequest, Branch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBranchRequest, Branch>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Branch, BranchResponse>()
                //.ForMember(des => des.AccountName, opt => opt.MapFrom<FullNameResolver>())
                .ReverseMap()
                .HandleNullProperty();
            CreateMap<Branch, CreateBranchResponse>().HandleNullProperty();
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
