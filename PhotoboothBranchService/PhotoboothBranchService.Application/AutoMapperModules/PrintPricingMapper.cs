using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PrintPricingMapper : Profile
    {
        public PrintPricingMapper()
        {
            CreateMap<CreatePrintPricingRequest, PrintPricing>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePrintPricingRequest, PrintPricing>().ReverseMap().HandleNullProperty();
            CreateMap<PrintPricing, PrintPricingResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
