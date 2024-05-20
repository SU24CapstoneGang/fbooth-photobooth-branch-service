using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhotoboothBranchService.Api.Common
{
    public class AdminAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var role = context.HttpContext.Items["Role"];
                if (role != null)
                {
                    if (role.ToString() == "ADMIN")
                        return;
                }
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
