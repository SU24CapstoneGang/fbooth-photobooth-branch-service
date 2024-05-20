using PhotoboothBranchService.Application.DTOs.RequestModels;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Common
{
    public class FilterPagingModel<TFilter>
    {
        public TFilter Filter { get; set; }
        public PagingModel Paging { get; set; }
    }
}
