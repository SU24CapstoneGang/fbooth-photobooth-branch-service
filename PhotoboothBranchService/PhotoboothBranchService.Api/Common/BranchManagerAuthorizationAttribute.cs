using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PhotoboothBranchService.Api.Common
{
    public class BranchManagerAuthorizationAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var role = context.HttpContext.Items["Role"];
                if (role != null)
                {
                    if (role.ToString() == "BRANCHMANAGER")
                        return;
                }
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
