using AutoMapper;
using PhotoboothBranchService.Application.Common.Helper;
using PhotoboothBranchService.Application.DTOs.RequestModels.PhotoBoothBranch;
using PhotoboothBranchService.Application.DTOs.ResponseModels.PhotoBoothBranch;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Session;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoBoothBranchMapper : Profile
    {
        public PhotoBoothBranchMapper()
        {
            CreateMap<CreatePhotoBoothBranchRequest, PhotoBoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePhotoBoothBranchRequest, PhotoBoothBranch>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoBoothBranch, PhotoBoothBranchresponse>()
                .ForPath(des => des.CameraModelName, opt => opt.MapFrom(src => src.Camera.ModelName))
                .ForPath(des => des.PrinterModelName, opt => opt.MapFrom(src => src.Printer.ModelName))
                .ForMember(des => des.AccountName, opt => opt.MapFrom<FullNameResolver>())
                .ReverseMap()
                .HandleNullProperty();
        }
    }

    public class FullNameResolver : IValueResolver<PhotoBoothBranch, PhotoBoothBranchresponse, string>
    {
        public string Resolve(PhotoBoothBranch source, PhotoBoothBranchresponse destination, string destMember, ResolutionContext context)
        {
            return $"{source.Account.FirstName} {source.Account.LastName}";
        }
    }
}
