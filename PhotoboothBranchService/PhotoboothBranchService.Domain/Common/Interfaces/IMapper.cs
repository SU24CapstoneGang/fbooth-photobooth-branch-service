namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
}
