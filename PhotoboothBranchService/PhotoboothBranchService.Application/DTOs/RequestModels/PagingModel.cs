namespace PhotoboothBranchService.Application.DTOs.RequestModels;

public class PagingModel
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
