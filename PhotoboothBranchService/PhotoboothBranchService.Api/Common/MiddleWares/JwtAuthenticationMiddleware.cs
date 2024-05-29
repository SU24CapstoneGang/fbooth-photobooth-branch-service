using FirebaseAdmin.Auth;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Api.Common.MiddleWares
{
    public class FirebaseAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccountRepository accountRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                if (decodedToken != null)
                {
                    var user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);
                    var email = user.Email;
                    var role = "ANONYMOUS";
                    var accountId = "";
                    if (email == JsonHelper.GetFromAppSettings("Admin:Email"))
                    {
                        role = "ADMIN";
                    }
                    else
                    {
                        var account = (await accountRepository.GetAsync(c => c.Email == email)).FirstOrDefault();
                        if (account != null)
                        {
                            role = account.Role.RoleName;
                            accountId = account.AccountID.ToString();
                        }
                    }
                    context.Items["Role"] = role;
                    context.Items["Email"] = email;
                    context.Items["Token"] = token;
                    context.Items["CustomerId"] = accountId;
                }
            };
            await _next(context);
        }
    }
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirebaseAuthenticationMiddleware>();
        }
    }
}

