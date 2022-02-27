using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
                where TEntity : class
    {
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllProduct(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);
            return exp is null
                   ? await query.ToListAsync()
                   : await query.Where(exp).ToListAsync();
        }
        private IQueryable<TEntity> GetQuery(params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query.Include(item);    
                    //query.Take(1);
                }
            }
            return query;
        }
    }
}