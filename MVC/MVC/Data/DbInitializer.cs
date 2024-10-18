/*using MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            Player one = new("Karen") { Token = "karen", Friends = { "Ernst", "John" } };
            Player two = new("Ernst") { Token = "ernst", Friends = { "John", "Karen" } };
            Player three = new("John") { Token = "john", Friends = { "Ernst", "Karen" } };
            Player four = new("Eltjo") { Token = "eltjo", Friends = { "Tijn" }, PendingFriends = { "Karen", "Ernst", "John" } };
            Player five = new("Tijn") { Token = "tijn", Friends = { "Eltjo" }, PendingFriends = { "Karen", "Ernst", "John" } };

            _builder.Entity<Player>().HasData(one, two, three, four, five);
        }
    }
}
*/