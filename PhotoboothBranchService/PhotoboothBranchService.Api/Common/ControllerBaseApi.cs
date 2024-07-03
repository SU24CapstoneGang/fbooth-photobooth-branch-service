using Microsoft.AspNetCore.Mvc;

namespace PhotoboothBranchService.Api.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBaseApi : ControllerBase
    {
    }
}
