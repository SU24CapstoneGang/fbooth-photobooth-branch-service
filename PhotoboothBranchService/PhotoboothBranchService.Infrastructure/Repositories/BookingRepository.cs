using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _dbContext;

    public BookingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking> GetBookingByValidateCodeAndBoothIdAsync(long validateCode, Guid boothId)
    {
        return await _dbContext.Bookings
            .Include(b => b.BookingServices)
            .ThenInclude(bs => bs.Service)
            .FirstOrDefaultAsync(b => b.ValidateCode == validateCode && b.BoothID == boothId);
    }


    // Add a new session
    public async Task<Booking> AddAsync(Booking session)
    {
        var result = await _dbContext.AddAsync(session);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    //Read
    public async Task<IQueryable<Booking>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Bookings);
    }

    public async Task<IQueryable<Booking>> GetAsync(
        Expression<Func<Booking, bool>> predicate = null,
        params Expression<Func<Booking, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Bookings : _dbContext.Bookings.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Booking>().AsQueryable());
            }
            else
            {
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        if (IncludeHelper.IsValidInclude(includeProperty))
                        {
                            result = result.Include(includeProperty);
                        }
                    }
                }
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // Remove a session
    public async Task RemoveAsync(Booking session)
    {
        _dbContext.Bookings.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    // Update a session
    public async Task UpdateAsync(Booking session)
    {
        _dbContext.Entry(session).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task updateTotalPrice(Guid SessionOrderID)
    {
        var order = _dbContext.Bookings.Where(i => i.BookingID == SessionOrderID)
            .Include(u => u.BookingServices)
            .FirstOrDefault();

        if (order != null)
        {
            decimal totalPrice = 0;
            foreach (var item in order.BookingServices)
            {
                totalPrice += item.Price * item.Quantity;

            }
            order.PaymentAmount = totalPrice;
            await UpdateAsync(order);
        }
    }
}

