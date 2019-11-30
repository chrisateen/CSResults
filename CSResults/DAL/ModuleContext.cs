using CSResults.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSResults.DAL
{
    public class ModuleContext:DbContext
    {
        public ModuleContext() : base("ModuleContext")
        {
            Database.SetInitializer<ModuleContext>(new CreateDatabaseIfNotExists<ModuleContext>());
        }

        public DbSet<Module> Module { get; set; }
        public DbSet<Result> Result { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Result>()
                .HasRequired<Module>(r => r.Module)
            .WithMany(m => m.Results)
            .HasForeignKey<String>(m => m.modID);
        }
    }
}
