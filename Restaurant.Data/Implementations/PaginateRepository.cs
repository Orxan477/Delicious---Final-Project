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
    public class PaginateRepository<T, U> : IPaginateRepository<T, U>
                where T : class
        where U : class
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public PaginateRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int GetPageCount(int take, Expression<Func<T, bool>> expression = null)
        {
            var prodCount=0;
            if (expression is null) prodCount = _context.Set<T>().Count();
            else prodCount = _context.Set<T>().Where(expression).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        public List<U> GetProductList(List<T> entity)
        {
            List<U> models = new List<U>();
            foreach (var item in entity)
            {
                U model = _mapper.Map<U>(item);
                models.Add(model);
            }
            return models;
        }

        

        public async Task<List<T>> GetPaginate(int count, int page = 1,  params string[] includes)
        {
            var query = GetQuery(includes);

            return await query.Skip((page - 1) * count)
                             .Take(count)
                             .ToListAsync();

                //: await query.Where(expression)
                //             .Skip((page - 1) * count)
                //             .Take(count)
                //             .ToListAsync();
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