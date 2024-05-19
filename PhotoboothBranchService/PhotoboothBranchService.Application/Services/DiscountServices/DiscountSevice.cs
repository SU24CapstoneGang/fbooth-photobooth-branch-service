using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Discount;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Discount;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.DiscountServices;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    //Create
    public async Task<Guid> CreateAsync(CreateDiscountRequest createModel)
    {
        var discount = _mapper.Map<Discount>(createModel);
        return await _discountRepository.AddAsync(discount);
    }

    // Get all Discounts
    public async Task<IEnumerable<Discountresponse>> GetAllAsync()
    {
        var discounts = await _discountRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Discountresponse>>(discounts.ToList());
    }

    public async Task<IEnumerable<Discountresponse>> GetAllPagingAsync(DiscountFilter filter, PagingModel paging)
    {
        var discounts = (await _discountRepository.GetAllAsync()).AutoPaging(paging.PageSize, paging.PageIndex);
        var listDiscountresponse = _mapper.Map<IEnumerable<Discountresponse>>(discounts.ToList());
        listDiscountresponse.AutoFilter(filter);
        return listDiscountresponse;
    }

    public async Task<Discountresponse> GetByCode(string code)
    {
        var discount = await _discountRepository.GetAsync(d => d.DiscountCode == code);
        return _mapper.Map<Discountresponse>(discount);
    }

    public async Task<Discountresponse> GetByIdAsync(Guid id)
    {
        var discount = (await _discountRepository.GetAsync(d => d.DiscountID == id)).FirstOrDefault();
        if (discount == null)
        {
            throw new KeyNotFoundException("Discount not found.");
        }
        return _mapper.Map<Discountresponse>(discount);
    }

    // Get Discounts by Code
    public async Task<IEnumerable<Discountresponse>> SearchByCode(string code)
    {
        var discounts = await _discountRepository.GetAsync(d => d.DiscountCode.Contains(code));
        return _mapper.Map<IEnumerable<Discountresponse>>(discounts.ToList());
    }

    //Update
    public async Task UpdateAsync(Guid id, UpdateDiscountRequest updateModel)
    {
        var discounts = await _discountRepository.GetAsync(d => d.DiscountID == id);
        var discount = discounts.FirstOrDefault();
        if (discount == null)
        {
            throw new KeyNotFoundException("Discount not found.");
        }

        var updatedDiscount = _mapper.Map(updateModel, discount);
        updatedDiscount.LastModified = DateTime.UtcNow;
        await _discountRepository.UpdateAsync(updatedDiscount);
    }

    // Delete a Discount by ID
    public async Task DeleteAsync(Guid id)
    {
        var discounts = await _discountRepository.GetAsync(d => d.DiscountID == id);
        var discount = discounts.FirstOrDefault();
        if (discount != null)
        {
            await _discountRepository.RemoveAsync(discount);
        }
        else
        {
            throw new KeyNotFoundException("Discount not found.");
        }
    }
}

