namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IService<responseModel, CreateModel, UpdateModel, Filter, Paging>
{
    public Task<responseModel> GetByIdAsync(Guid id);
    public Task<IEnumerable<responseModel>> GetAllAsync();
    public Task<IEnumerable<responseModel>> GetAllPagingAsync(Filter filter, Paging paging);
    public Task<Guid> CreateAsync(CreateModel createModel);
    public Task UpdateAsync(Guid id, UpdateModel updateModel);
    public Task DeleteAsync(Guid id);
}