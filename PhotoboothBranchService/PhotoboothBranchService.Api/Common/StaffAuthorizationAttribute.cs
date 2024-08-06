using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhotoboothBranchService.Api.Common
{
    public class StaffAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var role = context.HttpContext.Items["Role"];
                if (role != null)
                {
                    if (role.ToString() == "STAFF")
                        return;
                }
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
