using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
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
            .ForMember(des => des.AccountId, act => act.MapFrom(src=>src.AccountID))
            .ReverseMap();
        CreateMap<Camera, CameraDTO>()
            .ForMember(des => des.CameraId, act => act.MapFrom(src => src.CameraID))
            .ReverseMap();
        CreateMap<PhotoBoothBranch, PhotoBoothBranchDTO>()
            .ForMember(des => des.PhotoBoothBranchId, act => act.MapFrom(src => src.BranchesID))
            .ReverseMap();
        CreateMap<Printer, PrinterDTO>()
            .ForMember(des => des.PrinterId, act => act.MapFrom(src => src.PrinterID))
            .ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
    }
}