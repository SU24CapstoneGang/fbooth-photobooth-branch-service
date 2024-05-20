using FirebaseAdmin.Auth;
using PhotoboothBranchService.Application.Common.Helpers;

namespace PhotoboothBranchService.Api.Common.MiddleWares
{
    public class FirebaseAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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
                    var customerId = "";
                    if (email == JsonHelper.GetFromAppSettings("Admin:Email"))
                    {
                        role = "ADMIN";
                    }
                    else
                    {
                        //var customer = customerRepository.Get(c => c.Email == email).FirstOrDefault();
                        //if (customer != null)
                        //{
                        //    role = "CUSTOMER";
                        //    customerId = customer.CustomerId.ToString();
                        //}
                        //else
                        //{
                        //    var staff = staffRepository.Get(c => c.Email == email).FirstOrDefault();
                        //    if (staff != null)
                        //    {
                        //        role = "STAFF";
                        //    }
                        //}
                    }
                    context.Items["Role"] = role;
                    context.Items["Email"] = email;
                    context.Items["Token"] = token;
                    context.Items["CustomerId"] = customerId;
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

