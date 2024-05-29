using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Printer;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PrinterMapper : Profile
    {
        public PrinterMapper()
        {
            CreateMap<CreatePrinterRequest, Printer>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePrinterRequest, Printer>().ReverseMap().HandleNullProperty();
            CreateMap<PrinterResponse, Printer>().ReverseMap().HandleNullProperty();
        }
    }
}
