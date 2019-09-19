namespace Dal.Migrations
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dal.ATMDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Dal.ATMDbContext context)
        {

            context.ATMMachines.AddOrUpdate(
              a => a.Id,
              new ATMMachine { Location = "Ýstanbul", BesTL = 50, ElliTL = 30, IkiyuzTL = 10, OnTL = 40, YirmiTL = 20,YuzTL=10,TotalMoney=4750 }
            );

            context.Customers.AddOrUpdate(
                c => c.Id,
                new Customer { Password = "1234", TotalMoney = 3000 }
                );
            
        }
    }
}
