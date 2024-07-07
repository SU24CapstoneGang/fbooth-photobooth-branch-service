namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IServiceBase<responseModel, CreateModel, CreateResponseModel, UpdateModel, Filter, Paging>
{
    public Task<responseModel> GetByIdAsync(Guid id);
    public Task<IEnumerable<responseModel>> GetAllAsync();
    public Task<IEnumerable<responseModel>> GetAllPagingAsync(Filter filter, Paging paging);
    public Task<CreateResponseModel> CreateAsync(CreateModel createModel);
    public Task UpdateAsync(Guid id, UpdateModel updateModel);
    public Task DeleteAsync(Guid id);
}