using FirebaseAdmin.Auth;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Api.Common.MiddleWares
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccountRepository accountRepository, IBranchRepository branchRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var role = "ANONYMOUS";
                var accountId = Guid.Empty;

                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                if (decodedToken != null)
                {
                    var user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);
                    var email = user.Email;
                    if (email == JsonHelper.GetFromAppSettings("Admin:Email"))
                    {
                        role = "ADMIN";
                    }
                    else
                    {
                        var account = (await accountRepository.GetAsync(c => c.Email == email)).FirstOrDefault();
                        if (account != null)
                        {
                            role = account.Role.ToString();
                            accountId = account.AccountID;
                            if (account.Status == Domain.Enum.AccountStatus.Blocked)
                            {
                                throw new ForbiddenAccessException("Account has been blocked by system.");
                            }
                            if (account.Role == Domain.Enum.AccountRole.Staff)
                            {
                                if (!account.BranchID.HasValue)
                                {
                                    throw new ForbiddenAccessException("Account not belong any branch yet, contact admin to assign branch.");
                                } else {
                                    var branch = (await branchRepository.GetAsync(i => i.BranchID == account.BranchID)).Single();
                                    if (branch.Status == Domain.Enum.BranchStatus.Inactive)
                                    {
                                        throw new ForbiddenAccessException("Branch has been Inactive.");
                                    }
                                }
                            }
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
            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
        }
    }
}

