using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApiKeyAuthorize : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration["KEY"] ?? throw new InvalidOperationException("API key is not set.");

            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
