using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllProduct(Expression<Func<TEntity,bool>> exp=null,params string[] includes);
    }
}