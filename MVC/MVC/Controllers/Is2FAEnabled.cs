using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVC.Data;

namespace MVC.Controllers
{
    public class Is2FAEnabled : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();

            if (userManager != null)
            {
                var user = await userManager.GetUserAsync(context.HttpContext.User);

                if (user != null && !await userManager.GetTwoFactorEnabledAsync(user))
                {
                    context.Result = new RedirectResult("/Identity/Account/Manage/TwoFactorAuthentication", false);
                    return;
                }
            }

            await next();
        }
    }
}
