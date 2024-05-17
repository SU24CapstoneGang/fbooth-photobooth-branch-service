using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
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
        CreateMap<Account, CreateAccountRequestModel>().ReverseMap();
        CreateMap<Camera, CameraDTO>().ReverseMap();
        CreateMap<PhotoBoothBranch, PhotoBoothBranchDTO>().ReverseMap();
        CreateMap<Printer, PrinterDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<Layout, LayoutDTO>().ReverseMap();
        CreateMap<Filter, FilterDTO>().ReverseMap();
        CreateMap<Sticker, StickerDTO>().ReverseMap();
        CreateMap<Frame, FrameDTO>().ReverseMap();
    }
}