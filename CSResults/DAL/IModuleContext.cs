using CSResults.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSResults.DAL
{
    interface IModuleContext: IDisposable
    {
        DbSet<Module> Module { get; }
        DbSet<Result> Result { get; }
    }
}
