using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

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
            try
            {
                PrintPricing printPricing = _mapper.Map<PrintPricing>(createModel);
                return await _printPricingRepository.AddAsync(printPricing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create price: " + ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var printPricing = (await _printPricingRepository.GetAsync(p => p.PrintPricingID == id)).FirstOrDefault();
                if (printPricing == null)
                {
                    throw new NotFoundException("PrintPricing", id, "Print pricing not found");
                }
                await _printPricingRepository.RemoveAsync(printPricing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleted price: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PrintPricingResponse>> GetAllAsync()
        {
            try
            {
                var printPricings = await _printPricingRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting print price: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PrintPricingResponse>> GetAllPagingAsync(PrintPricingFilter filter, PagingModel paging)
        {
            try
            {
                var printPricings = (await _printPricingRepository.GetAllAsync()).ToList().AutoFilter(filter);
                var printPricingsResponse = _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings);
                printPricingsResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
                return printPricingsResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting print price: " + ex.Message);
            }
        }

        public async Task<PrintPricingResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var printPricings = (await _printPricingRepository.GetAsync(p => p.PrintPricingID == id)).FirstOrDefault();
                if (printPricings == null)
                    throw new NotFoundException("PrintPricing", id, "Print pricing not found");

                return _mapper.Map<PrintPricingResponse>(printPricings);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while get print price: " + ex.Message);
            }

        }

        //public async Task<IEnumerable<PrintPricingResponse>> GetByName(string name)
        //{
        //    var printPricings = await _printPricingRepository.GetAsync(p => p.UnitPrice.Contains(name));
        //    return _mapper.Map<IEnumerable<PrintPricingResponse>>(printPricings.ToList());
        //}

        public async Task UpdateAsync(Guid id, UpdatePrintPricingRequest updateModel)
        {
            try
            {
                var printPricing = (await _printPricingRepository.GetAsync(p => p.PrintPricingID == id)).FirstOrDefault();
                if (printPricing == null)
                {
                    throw new NotFoundException("PrintPricing", id, "Print pricing not found");
                }

                var updatedPrintPricing = _mapper.Map(updateModel, printPricing);
                await _printPricingRepository.UpdateAsync(updatedPrintPricing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update print price: " + ex.Message);
            }

        }
    }
}
