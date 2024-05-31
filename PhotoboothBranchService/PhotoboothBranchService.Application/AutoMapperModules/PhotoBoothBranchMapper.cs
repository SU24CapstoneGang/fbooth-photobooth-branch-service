using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoBoothBranch;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoBoothBranchMapper : Profile
    {
        public PhotoBoothBranchMapper()
        {
            CreateMap<CreatePhotoBoothBranchRequest, PhotoBoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePhotoBoothBranchRequest, PhotoBoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoBoothBranch, PhotoBoothBranchresponse>()
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
