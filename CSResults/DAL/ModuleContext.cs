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
        }

        public DbSet<Module> Module { get; set; }
        public DbSet<Result> Result { get; set; }
    }
}
