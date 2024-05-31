using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PrintPricingServices
{
    public interface IPrintPricingService : IService<PrintPricingResponse, CreatePrintPricingRequest, UpdatePrintPricingRequest, PrintPricingFilter, PagingModel>
    {

    }
}
