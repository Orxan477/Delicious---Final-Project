using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IPaginateRepository<T>
    {
        Task<List<T>> GetPaginate(int count, int page, Expression<Func<T, bool>> exp = null, params string[] includes );
        int GetPageCount(int take, Expression<Func<T, bool>> expression = null);
    }
}