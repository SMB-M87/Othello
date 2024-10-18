using MVC.Data;
using MVC.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly Database _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(/*Database context, */UserManager<IdentityUser> userManager)
        {
            //_context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();

/*            if (currentUserID != null)
            {
                var speler = _context.Players.FirstOrDefault(s => s.Token == currentUserID);
                if (speler == null)
                {
                    var user = _userManager.FindByIdAsync(currentUserID).Result;
                    var nieuweSpeler = new Player (user.UserName) { Token = currentUserID };

                    _context.Players.Add(nieuweSpeler);
                    _context.SaveChanges();
                }
            }*/

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
