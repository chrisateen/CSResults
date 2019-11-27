using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CSResults.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ModuleContext context;
        private DbSet<T> dbSet;

        public GenericRepository(ModuleContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> Get(
                        Expression<Func<T, bool>> filter = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;

            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            //If there is no orderBy specifed return the unordered query
            else
            {
                return query.ToList();
            }
        }

    }
}