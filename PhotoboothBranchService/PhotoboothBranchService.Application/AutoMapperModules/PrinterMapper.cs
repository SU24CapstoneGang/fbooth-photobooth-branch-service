using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Printer;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Printer;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PrinterMapper : Profile
    {
        public PrinterMapper() {
            CreateMap<CreatePrinterRequest,Printer>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePrinterRequest, Printer>().ReverseMap().HandleNullProperty();
            CreateMap<PrinterResponse, Printer>()
                .ForPath(des => des.PhotoBoothBranch.BranchName, opt => opt.MapFrom(src => src.PhotoBoothBranchName))
                .ReverseMap().HandleNullProperty();
        }
    }
}
