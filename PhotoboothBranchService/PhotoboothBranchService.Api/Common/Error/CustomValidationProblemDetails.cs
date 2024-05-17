using Microsoft.AspNetCore.Mvc;

namespace PhotoboothBranchService.Api.Common.Error
{
    public class CustomProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
