using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BiografCSharpTest.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BiografCSharpTest.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // denne klasse sørger for at opdatere LastActive tiden
            var resultContext = await next();

            var claimUser = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claimUser == null) {
                return;
            }

            var userId = int.Parse(claimUser.Value);

            var userRepo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

            var user = await userRepo.GetUser(userId);

            if (user != null) {
                user.LastActive = DateTime.Now;

                await userRepo.SaveAll();
            }
        }
    }
}