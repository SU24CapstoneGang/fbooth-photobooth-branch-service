using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Discount;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
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
        try
        {
            var isDiscountExisted = await _discountRepository.GetAsync(d => d.DiscountCode == createModel.DiscountCode);
            if (isDiscountExisted != null) throw new BadRequestException("Discount code is already existed");

            var discount = _mapper.Map<Discount>(createModel);
            discount.Status = DiscountStatus.Active;
            return await _discountRepository.AddAsync(discount);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while create discount: " + ex.Message);
        }
    }

    // Get all Discounts
    public async Task<IEnumerable<Discountresponse>> GetAllAsync()
    {
        try
        {
            var discounts = await _discountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Discountresponse>>(discounts.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the discount: " + ex.Message);
        }
    }

    public async Task<IEnumerable<Discountresponse>> GetAllPagingAsync(DiscountFilter filter, PagingModel paging)
    {
        try
        {
            var discounts = (await _discountRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listDiscountresponse = _mapper.Map<IEnumerable<Discountresponse>>(discounts);
            listDiscountresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listDiscountresponse;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the discount: " + ex.Message);
        }
    }

    public async Task<Discountresponse> GetByCode(string code)
    {
        try
        {
            var discount = await _discountRepository.GetAsync(d => d.DiscountCode == code);
            if (discount == null)
                throw new NotFoundException("discount", code, "discount code not found");

            return _mapper.Map<Discountresponse>(discount);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the discount: " + ex.Message);
        }
    }

    public async Task<Discountresponse> GetByIdAsync(Guid id)
    {
        try
        {
            var discount = (await _discountRepository.GetAsync(d => d.DiscountID == id)).FirstOrDefault();
            if (discount == null)
            {
                throw new NotFoundException("discount", id, "discount id not found");
            }
            return _mapper.Map<Discountresponse>(discount);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the discount: " + ex.Message);
        }
    }

    // Get Discounts by Code
    public async Task<IEnumerable<Discountresponse>> SearchByCode(string code)
    {
        try
        {
            var discounts = await _discountRepository.GetAsync(d => d.DiscountCode.Contains(code));
            return _mapper.Map<IEnumerable<Discountresponse>>(discounts.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the discount: " + ex.Message);
        }
    }

    //Update
    public async Task UpdateAsync(Guid id, UpdateDiscountRequest updateModel)
    {
        try
        {
            var discount = (await _discountRepository.GetAsync(d => d.DiscountID == id)).FirstOrDefault();
            if (discount == null)
            {
                throw new NotFoundException("discount", id, "discount id not found");
            }

            var updatedDiscount = _mapper.Map(updateModel, discount);
            updatedDiscount.LastModified = DateTime.UtcNow;
            await _discountRepository.UpdateAsync(updatedDiscount);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while update discount: " + ex.Message);
        }
    }

    // Delete a Discount by ID
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var discounts = await _discountRepository.GetAsync(d => d.DiscountID == id);
            var discount = discounts.FirstOrDefault();
            if (discount == null)
            {
                throw new NotFoundException("discount", id, "discount id not found");
            }
            await _discountRepository.RemoveAsync(discount);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting discount: " + ex.Message);
        }
    }
}

