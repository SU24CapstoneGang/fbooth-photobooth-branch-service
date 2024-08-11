using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class BookingSlotRepository : IBookingSlotRepository
    {
        private readonly AppDbContext _dbContext;

        public BookingSlotRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<BookingSlot> AddAsync(BookingSlot entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<BookingSlot>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.BookingSlots.AsQueryable());
        }

        public async Task<IQueryable<BookingSlot>> GetAsync(
            Expression<Func<BookingSlot, bool>> predicate = null,
            params Expression<Func<BookingSlot, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.BookingSlots : _dbContext.BookingSlots.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<BookingSlot>().AsQueryable());
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

        // Update
        public async Task UpdateAsync(BookingSlot entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(BookingSlot entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
