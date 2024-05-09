using Microsoft.AspNetCore.Mvc;

namespace PhotoboothBranchService.Api.Error
{
    public class CustomProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
