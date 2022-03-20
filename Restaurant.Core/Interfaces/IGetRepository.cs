using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IGetRepository<TEntity>
    {
        Task<List<TEntity>> GetPaginate(int count, int page, Expression<Func<TEntity, bool>> exp = null, params string[] includes );
        int GetPageCount(int take, Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
    }
}