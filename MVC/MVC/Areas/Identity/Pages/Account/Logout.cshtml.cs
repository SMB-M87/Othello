#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(
            UserManager<ApplicationUser> userManager, 
            ILogger<LoginModel> logger, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToPage("/Home/Index");
        }
    }
}
