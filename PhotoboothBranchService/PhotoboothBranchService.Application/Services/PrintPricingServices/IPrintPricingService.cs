using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PrintPricingServices
{
    public interface IPrintPricingService : IService<PrintPricingResponse, CreatePrintPricingRequest, UpdatePrintPricingRequest, PrintPricingFilter, PagingModel>
    {
        Task<IEnumerable<PrintPricingResponse>> GetByName(string name);
    }
}
