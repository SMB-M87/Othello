using Microsoft.AspNetCore.Identity;

namespace MVC.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@othello.com";
            string password = "Admin123!";

            string mediatorEmail = "mediator@othello.com";
            string mediatorPassword = "Mediator01!";

            if (await roleManager.FindByNameAsync("administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("administrator"));
            }
            if (await roleManager.FindByNameAsync("mediator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("mediator"));
            }
            if (await roleManager.FindByNameAsync("player") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("player"));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                IdentityUser admin = new() { Email = adminEmail, UserName = "admin" };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "administrator");
                }
            }

            if (await userManager.FindByEmailAsync(mediatorEmail) == null)
            {
                IdentityUser mediator = new() { Email = mediatorEmail, UserName = "mediator" };
                IdentityResult result = await userManager.CreateAsync(mediator, mediatorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mediator, "mediator");
                }
            }
        }
    }

}
