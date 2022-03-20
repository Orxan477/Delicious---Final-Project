using AutoMapper;
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
    public class GetRepository<TEntity> : IGetRepository<TEntity>
                where TEntity : class
    {
        private AppDbContext _context;

        public GetRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetPageCount(int take, Expression<Func<TEntity, bool>> expression = null)
        {
            var prodCount=0;
            if (expression is null) prodCount = _context.Set<TEntity>().Count();
            else prodCount = _context.Set<TEntity>().Where(expression).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        public async Task<List<TEntity>> GetPaginate(int count, int page, Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);

            return exp is null
                ?await query.Skip((page - 1) * count)
                             .Take(count)
                             .ToListAsync()
                : await query.Where(exp)
                             .Skip((page - 1) * count)
                             .Take(count)
                             .ToListAsync();
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> exp , params string[] includes)
        {
            var query = GetQuery(includes);

            return await query.Where(exp).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!(includes is null))
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);

            return exp is null
                ? await query.ToListAsync()
                : await query.Where(exp).ToListAsync();
        }
    }
}