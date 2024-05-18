using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    // Create a new Discount
    public async Task<Guid> CreateAsync(DiscountDTO entityDTO)
    {
        var discount = _mapper.Map<Discount>(entityDTO);
        return await _discountRepository.AddAsync(discount);
    }

    // Delete a Discount by ID
    public async Task DeleteAsync(Guid id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);
        if (discount != null)
        {
            await _discountRepository.RemoveAsync(discount);
        }
        else
        {
            throw new KeyNotFoundException("Discount not found.");
        }
    }

    // Get all Discounts
    public async Task<IEnumerable<DiscountDTO>> GetAllAsync()
    {
        var discounts = await _discountRepository.GetAll();
        return _mapper.Map<IEnumerable<DiscountDTO>>(discounts);
    }

    // Get Discounts by Code
    public async Task<IEnumerable<DiscountDTO>> GetByCode(string code)
    {
        var discounts = await _discountRepository.GetByCode(code);
        return _mapper.Map<IEnumerable<DiscountDTO>>(discounts);
    }

    public async Task<DiscountDTO> GetByIdAsync(Guid id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);
        if (discount == null)
        {
            throw new KeyNotFoundException("Discount not found.");
        }
        return _mapper.Map<DiscountDTO>(discount);
    }

    public async Task UpdateAsync(Guid id, DiscountDTO entityDTO)
    {
        var discount = await _discountRepository.GetByIdAsync(id);
        if (discount == null)
        {
            throw new KeyNotFoundException("Discount not found.");
        }

        var updatedDiscount = _mapper.Map(entityDTO, discount);
        updatedDiscount.LastModified = DateTime.UtcNow;
        await _discountRepository.UpdateAsync(updatedDiscount);
    }
}

