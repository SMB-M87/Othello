using MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVC.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Players()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7023/api/role/all");

            if (response.IsSuccessStatusCode)
            {
                var players = await response.Content.ReadFromJsonAsync<List<Player>>();
                return View(players);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve players.");
            return View(new List<Player>());
        }

        // GET: Admin/EditPlayer/{id}
        public async Task<IActionResult> EditPlayer(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7023/api/role/{id}");

            if (response.IsSuccessStatusCode)
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                return View(player);
            }

            return NotFound();
        }

        // POST: Admin/EditPlayer/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlayer(string id, Player updatedPlayer)
        {
            if (id != updatedPlayer.Token)
                return BadRequest();

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7023/api/role/{id}", updatedPlayer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Players));
            }

            ModelState.AddModelError(string.Empty, "Unable to update player.");
            return View(updatedPlayer);
        }

        // GET: Admin/DeletePlayer/{id}
        public async Task<IActionResult> DeletePlayer(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7023/api/role/{id}");

            if (response.IsSuccessStatusCode)
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                return View(player);
            }

            return NotFound();
        }

        // POST: Admin/DeletePlayer/{id}
        [HttpPost, ActionName("DeletePlayer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlayerConfirmed(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7023/api/role/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Players));
            }

            ModelState.AddModelError(string.Empty, "Unable to delete player.");
            return RedirectToAction(nameof(Players));
        }

        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("Admin123!");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "YourIssuer",
                Audience = "YourAudience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
