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
            Player one = new("karen", "Karen", 1);
            Player two = new("ernst", "Ernst", 1);
            Player three = new("john", "John", 1);
            Player four = new("eltjo", "Eltjo", 1);
            Player five = new("tijn", "Tijn", 1);
            Player six = new("cena", "Cena", 1);
            Player seven = new("burst", "Burst", 1);
            Player eight = new("burton", "Burton", 1);
            Player nine = new("briar", "Briar", 1);
            Player ten = new("lambert", "Lambert", 1);
            Player eleven = new("identity", "Identity", 1);
            Player t12 = new("salie", "Salie", 1);
            Player t13 = new("pipo", "Pipo", 1);
            Player t14 = new("gissa", "Gissa", 1);
            Player t15 = new("hidde", "Hidde", 1);
            Player t16 = new("noga", "Noga", 1);
            Player t17 = new("nastrovia", "Nastrovia", 1);
            Player t18 = new("pedro", "Pedro", 1);
            Player t19 = new("ahmed", "Ahmed", 1);
            Player t20 = new("nadege", "Nadege", 1);
            Player t21 = new("rachel", "Rachel", 1);
            Player t22 = new("ff20c418-f1b0-4f16-b582-294be25c24ef", "mediator"); // Give own identity mediator token
            Player t23 = new("58a479fd-ae6f-4474-a147-68cbdb62c19b", "admin"); // Give own identity admin token
            Player delete = new("deleted", "Deleted");

            _builder.Entity<Player>().HasData(one, two, three, four, five, six, seven, eight, nine, ten, eleven, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23, delete);
        }
    }
}