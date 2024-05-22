using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class FinalPictureRepository : IFinalPictureRepository
    {
        private readonly AppDbContext _dbContext;

        public FinalPictureRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(FinalPicture entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PictureID;
        }

        // Read
        public async Task<IQueryable<FinalPicture>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.FinalPictures.AsQueryable());
        }

        public async Task<IQueryable<FinalPicture>> GetAsync(Expression<Func<FinalPicture, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.FinalPictures.Where(predicate).AsQueryable());
        }

        // Update
        public async Task UpdateAsync(FinalPicture entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(FinalPicture entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
