#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MVC.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(
            IConfiguration configuration,
            ILogger<RegisterModel> logger,
            IHttpClientFactory httpClientFactory,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _userStore = userStore;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            var apiKey = configuration["ApiSettings:KEY"];
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [NormalizedPassword]
            [StringLength(65, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 16)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [StringLength(65, ErrorMessage = "The {0} is not allowed to exceed {1} characters.")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/register/{Input.Username}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ModelState.AddModelError(string.Empty, "Username is already taken.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    if (ModelState.IsValid)
                    {
                        var user = CreateUser();

                        await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);

                        var result = await _userManager.CreateAsync(user, Input.Password);

                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");
                            await _userManager.AddToRoleAsync(user, "user");

                            var playerCreation = new
                            {
                                ReceiverUsername = user.Id,
                                SenderToken = Input.Username
                            };

                            var createPlayerResponse = await _httpClient.PostAsJsonAsync("api/register", playerCreation);
                            if (createPlayerResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                ModelState.AddModelError(string.Empty, "An error occurred while creating the player in the API.");
                                await _userManager.DeleteAsync(user);
                            }
                            else if (createPlayerResponse.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return RedirectToPage("/Home/Index");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "An error occurred while creating the player.");
                                await _userManager.DeleteAsync(user);
                            }
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
