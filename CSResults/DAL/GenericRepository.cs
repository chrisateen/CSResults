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
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Checks if we have objects to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = dbSet.Include(include);
                }

            }

            //Orders the query if there is an orderby specifed
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
            
            
        }

        public IEnumerable<T> Get(
                        Expression<Func<T, bool>> filter = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                        params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Checks if we have objects to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = dbSet.Include(include);
                }
            }

            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var res = query.ToList();
            return res;
        }

    }
}