namespace MVC.Middleware
{
    public class SessionTimeout
    {
        private readonly RequestDelegate _next;

        public SessionTimeout(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true && !context.Session.Keys.Any())
            {
                context.Response.Redirect("/Identity/Account/Login?timeout=true");
                return;
            }

            await _next(context);
        }
    }
}
