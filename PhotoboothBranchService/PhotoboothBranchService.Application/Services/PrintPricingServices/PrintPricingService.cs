using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PrintPricingServices
{
    public class PrintPricingService : IPrintPricingService
    {
        private readonly IPrintPricingRepository _printPricingRepository;
        private readonly IMapper _mapper;

        public PrintPricingService(IPrintPricingRepository printPricingRepository, IMapper mapper)
        {
            _printPricingRepository = printPricingRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreatePrintPricingRequest createModel)
        {
            PrintPricing printPricing = _mapper.Map<PrintPricing>(createModel);
            return await _printPricingRepository.AddAsync(printPricing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var printPricing = await _printPricingRepository.GetAsync(p => p.PrintPricingID == id);
            var printPricingToDelete = printPricing.FirstOrDefault();
            if (printPricingToDelete != null)
            {
                await _printPricingRepository.RemoveAsync(printPricingToDelete);
            }
        }

        public async Task<IEnumerable<PrintPricingResponse>> GetAllAsync()
        {
            var printPricings = await _printPricingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings.ToList());
        }

        public async Task<IEnumerable<PrintPricingResponse>> GetAllPagingAsync(PrintPricingFilter filter, PagingModel paging)
        {
            var printPricings = (await _printPricingRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var printPricingsResponse = _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings);
            printPricingsResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return printPricingsResponse;
        }

        public async Task<PrintPricingResponse> GetByIdAsync(Guid id)
        {
            var printPricings = await _printPricingRepository.GetAsync(p => p.PrintPricingID == id);
            return _mapper.Map<PrintPricingResponse>(printPricings);
        }

        public async Task<IEnumerable<PrintPricingResponse>> GetByName(string name)
        {
            var printPricings = await _printPricingRepository.GetAsync(p => p.PrintName.Contains(name));
            return _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings.ToList());
        }

        public async Task UpdateAsync(Guid id, UpdatePrintPricingRequest updateModel)
        {
            var printPricing = (await _printPricingRepository.GetAsync(p => p.PrintPricingID == id)).FirstOrDefault();
            if (printPricing == null)
            {
                throw new KeyNotFoundException("Print pricing not found.");
            }

            var updatedPrintPricing = _mapper.Map(updateModel, printPricing);
            await _printPricingRepository.UpdateAsync(updatedPrintPricing);
        }
    }
}
