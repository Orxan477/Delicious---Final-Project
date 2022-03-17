using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IPaginateRepository<T, U>
    {
        //string GetSetting(string entity);  
        Task<List<T>> GetPaginate(int count , int page = 1,  params string[] includes);
        int GetPageCount(int take, Expression<Func<T, bool>> expression = null);
        List<U> GetProductList(List<T> products);
        //Task<List<TEntity>> GetAllProduct(Expression<Func<TEntity,bool>> exp=null,params string[] includes);
    }
}