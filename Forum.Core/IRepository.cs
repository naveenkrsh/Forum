using Forum.Core.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Forum.Core
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        //Task DeleteAsync(TEntity item); 
        Task DeleteAllAsync();
        Task<TEntity> Single(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> All(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> All();
        Task<IEnumerable<TEntity>> GetAllAsync();
        //System.Linq.IQueryable<T> All<T>(int page, int pageSize);
        Task<TEntity> AddAsync(TEntity item) ;//where T : class, new();
        //void Add<T>(IEnumerable<T> items);

        Task UpdateAsync(TEntity item);
        Task UpdateAsync(Expression<Func<TEntity, bool>> expression,TEntity item);
        Task AddSubDocument<TFeild,TItem>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TFeild>> action ,TItem value);
    }
}