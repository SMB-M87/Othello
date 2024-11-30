using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Ignore(e => e.Email);
                entity.Ignore(e => e.NormalizedEmail);
                entity.Ignore(e => e.EmailConfirmed);
                entity.Ignore(e => e.PhoneNumber);
                entity.Ignore(e => e.PhoneNumberConfirmed);
            });
        }
    }
}
