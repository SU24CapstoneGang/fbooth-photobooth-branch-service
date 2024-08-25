namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IServiceBase<responseModel, Filter, Paging>
{
    public Task<responseModel> GetByIdAsync(Guid id);
    public Task<IEnumerable<responseModel>> GetAllAsync();
    public Task<IEnumerable<responseModel>> GetAllPagingAsync(Filter filter, Paging paging);

}   