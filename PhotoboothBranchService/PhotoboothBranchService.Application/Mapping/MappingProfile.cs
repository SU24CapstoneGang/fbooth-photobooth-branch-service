using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhotoboothBranchService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

        //CreateMap<Camera, CameraDTO>()
        //    .ForMember(des => des.CameraId, act => act.MapFrom(src => src.Id))
        //    .ReverseMap();
        //CreateMap<PhotoBoothBranch, PhotoBoothBranchDTO>()
        //    .ForMember(des => des.PhotoBoothBranchId, act => act.MapFrom(src => src.Id))
        //    .ReverseMap();
        //CreateMap<Printer, PrinterDTO>()
        //    .ForMember(des => des.PrinterId, act => act.MapFrom(src => src.Id))
        //    .ReverseMap();
    }
}