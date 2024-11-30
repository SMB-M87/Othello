using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
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
            Player one = new("mary", "Mary", 2);
            Player three = new("john", "John", 3);
            Player four = new("jimmy", "Jimmy", 2);
            Player five = new("ted", "Ted", 4);
            Player six = new("michael", "Michael", 3);
            Player seven = new("william", "William", 1);
            Player eight = new("sarah", "Sarah", 3);
            Player nine = new("lisa", "Lisa", 1);
            Player ten = new("nancy", "Nancy", 1);
            Player eleven = new("Anthony", "Anthony", 2);
            Player t12 = new("matthew", "Matthew", 1);
            Player t13 = new("donald", "Donald", 1);
            Player t14 = new("andrew", "Andrew", 1);
            Player t15 = new("kimberly", "Kimberly", 1);
            Player t16 = new("margaret", "Margaret", 1);
            Player t17 = new("carol", "Carol", 1);
            Player t18 = new("brian", "Brian", 1);
            Player t19 = new("jason", "Jason", 1);
            Player t20 = new("jeffrey", "Jeffrey", 1);
            Player t21 = new("amy", "Amy", 1);
            Player delete = new("deleted", "Deleted");

            _builder.Entity<Player>().HasData(one, three, four, five, six, seven, eight, nine, ten, eleven, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, delete);
        }
    }
}