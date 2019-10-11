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

            var module = new List<Module>
            {
                new Module{moduleID = "Test 1", moduleName = "Test 1 Module"},
                new Module{moduleID = "Test 2", moduleName = "Test 2 Module"}

            };

            module.ForEach(s => context.Module.AddOrUpdate(p => p.moduleID, s));
            context.SaveChanges();

            var results = new List<Result>
            {
                new Result
                {
                    modID = module.Single(s => s.moduleID == "Test 1").moduleID,
                    modName = module.Single(s => s.moduleID == "Test 1").moduleName,
                    year = "2018/19",
                    mean = 55.12,
                    median = 54
                },

                 new Result
                {
                    modID = module.Single(s => s.moduleID == "Test 2").moduleID,
                    modName = module.Single(s => s.moduleID == "Test 2").moduleName,
                    year = "2017/18",
                    mean = 57.72,
                    median = 56
                }
            };

            results.ForEach(s => context.Result.AddOrUpdate(p => new { p.modID,p.year }, s));
            context.SaveChanges();

        }
    }
}