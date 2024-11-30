using Microsoft.AspNetCore.Identity;

namespace MVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public override string Email { get => null!; set { } }
        public override string NormalizedEmail { get => null!; set { } }
        public override bool EmailConfirmed { get => false; set { } }
        public override string PhoneNumber { get => null!; set { } }
        public override bool PhoneNumberConfirmed { get => false; set { } }
    }
}
