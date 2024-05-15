namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IService<TEntityDTO>
{
    Task<TEntityDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntityDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Guid> CreateAsync(TEntityDTO entity, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, TEntityDTO entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}