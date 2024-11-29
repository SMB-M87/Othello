using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = Guid.NewGuid().ToString();
            var modRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            var adminRole = "admin";
            var modRole = "mod";
            var userRole = "user";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = adminRole, NormalizedName = adminRole.ToUpper() },
                new IdentityRole { Id = modRoleId, Name = modRole, NormalizedName = modRole.ToUpper() },
                new IdentityRole { Id = userRoleId, Name = userRole, NormalizedName = userRole.ToUpper() }
            );

            var adminId = $"{adminRole}-133742069420135";
            var moderatorId = $"{modRole}-987456269420135";
            var userId = $"{userRole}-987456456198135";

            var hasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                Id = adminId,
                UserName = "OthelloMaster",
                NormalizedUserName = adminRole.ToUpper(),
                Email = $"{adminRole}@othello.com",
                NormalizedEmail = $"{adminRole.ToUpper()}@OTHELLO.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new(), adminRole),
            };

            var mediatorUser = new IdentityUser
            {
                Id = moderatorId,
                UserName = "OthelloMod",
                NormalizedUserName = modRole.ToUpper(),
                Email = $"{modRole}@othello.com",
                NormalizedEmail = $"{modRole.ToUpper()}@OTHELLO.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new(), modRole),
            };

            var userUser = new IdentityUser
            {
                Id = userId,
                UserName = "OthelloWorld",
                NormalizedUserName = "OTHELLOWORLD",
                Email = $"{userId}@othello.com",
                NormalizedEmail = $"{userId.ToUpper()}@OTHELLO.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new(), userRole),
            };

            builder.Entity<IdentityUser>().HasData(adminUser, mediatorUser, userUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminId, RoleId = adminRoleId },
                new IdentityUserRole<string> { UserId = adminId, RoleId = userRoleId },

                new IdentityUserRole<string> { UserId = moderatorId, RoleId = modRoleId },
                new IdentityUserRole<string> { UserId = moderatorId, RoleId = userRoleId },

                new IdentityUserRole<string> { UserId = userId, RoleId = userRoleId }
            );
        }
    }
}
