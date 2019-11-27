using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSResults.DAL
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll();

        T GetByModName(string modName);

        T GetByModID(string modID);
    }
}
