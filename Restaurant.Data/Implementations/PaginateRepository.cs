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
    public class PaginateRepository<T> : IPaginateRepository<T>
                where T : class
    {
        private AppDbContext _context;

        public PaginateRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetPageCount(int take, Expression<Func<T, bool>> expression = null)
        {
            var prodCount=0;
            if (expression is null) prodCount = _context.Set<T>().Count();
            else prodCount = _context.Set<T>().Where(expression).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        public async Task<List<T>> GetPaginate(int count, int page, Expression<Func<T, bool>> exp = null, params string[] includes)
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
        private IQueryable<T> GetQuery(string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (!(includes is null))
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}