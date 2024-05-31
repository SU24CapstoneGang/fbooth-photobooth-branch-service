using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhotoboothBranchService.Api.Common
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public AuthorizationAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var role = context.HttpContext.Items["Role"] as string;

            if (role == null || !_allowedRoles.Select(r => r.ToLower()).Contains(role.ToLower()))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
