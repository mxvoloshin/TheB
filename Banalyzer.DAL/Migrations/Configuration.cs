using System.Collections.Generic;
using Banalyzer.Domain.Common;

namespace Banalyzer.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Banalyzer.DAL.BanalyzerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Banalyzer.DAL.BanalyzerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //fill currencies
            if (!context.Currencies.Any())
            {
                context.Currencies.AddRange(new List<Currency>()
                {
                    new Currency {Code = "UAH", Description = "Hrivna"},
                    new Currency {Code = "USD", Description = "Dollar"},
                    new Currency {Code = "EUR", Description = "Euro"}
                });
            }
            //context.Currencies AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);
            
        }
    }
}
