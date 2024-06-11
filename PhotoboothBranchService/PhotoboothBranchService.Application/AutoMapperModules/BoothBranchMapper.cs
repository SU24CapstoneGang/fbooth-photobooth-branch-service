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
                //.ForPath(des => des.CameraModelName, opt => opt.MapFrom(src => src.Camera.ModelName))
                //.ForPath(des => des.PrinterModelName, opt => opt.MapFrom(src => src.Printer.ModelName))
                //.ForMember(des => des.AccountName, opt => opt.MapFrom<FullNameResolver>())
                .ReverseMap()
                .HandleNullProperty();
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
