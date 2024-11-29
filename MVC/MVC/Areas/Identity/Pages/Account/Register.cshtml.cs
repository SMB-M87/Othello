﻿#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace MVC.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterModel(
            IEmailSender emailSender,
            ILogger<RegisterModel> logger,
            IUserStore<IdentityUser> userStore,
            IHttpClientFactory httpClientFactory,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _logger = logger;
            _userStore = userStore;
            _emailSender = emailSender;
            _userManager = userManager;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [NormalizedPassword]
            [StringLength(65, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 12)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [StringLength(65, ErrorMessage = "The {0} is not allowed to exceed {1} characters.")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var response = await _httpClient.GetAsync($"api/check/{Input.Username}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ModelState.AddModelError(string.Empty, "Username is already taken.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                if (ModelState.IsValid)
                {
                    var user = CreateUser();

                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
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
                            var userId = await _userManager.GetUserIdAsync(user);
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId, code, returnUrl },
                                protocol: Request.Scheme);

                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                            if (_userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                            }
                            else
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(returnUrl);
                            }
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

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
