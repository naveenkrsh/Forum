using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Forum.Services.Contract
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        Task<T> SaveAsync(T tag);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
    }
}