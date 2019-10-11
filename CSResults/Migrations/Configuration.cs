namespace CSResults.Migrations
{
    using CSResults.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSResults.DAL.ModuleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CSResults.DAL.ModuleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Module.AddOrUpdate(x => x.moduleID,
                new Module()
                {
                    moduleID = "Test",
                    moduleName = "Test Data",
                    Results = new List<Result>
                {
                    new Result()
                    {
                        modName = "Test Data",
                        year = "2017/18",
                        mean = 52.5
                    }
                }

                }
                );
        }
    }
}
