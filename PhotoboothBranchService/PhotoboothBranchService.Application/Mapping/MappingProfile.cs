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
        CreateMap<Accounts, AccountDTO>()
            .ForMember(des => des.AccountId, act => act.MapFrom(src=>src.Id))
            .ForMember(des => des.PhotoBoothBrachId, act => act.MapFrom(src => src.PhotoBoothBranchId))
            .ReverseMap();
        CreateMap<Cameras, CameraDTO>()
            .ForMember(des => des.CameraId, act => act.MapFrom(src => src.Id))
            .ReverseMap();
        CreateMap<PhotoBoothBranches, PhotoBoothBranchDTO>()
            .ForMember(des => des.PhotoBoothBranchId, act => act.MapFrom(src => src.Id))
            .ReverseMap();
        CreateMap<Printers, PrinterDTO>()
            .ForMember(des => des.PrinterId, act => act.MapFrom(src => src.Id))
            .ReverseMap();
    }
}