using Forum.Infrastructure.Contract;
using Forum.Core;
using Forum.Core.Models.Bases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
namespace Forum.Test.InMemory
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private Dictionary<string, List<TEntity>> db;

        static InMemoryRepository()
        {
            
        }
        public InMemoryRepository()
        {
            db = new Dictionary<string, List<TEntity>>();
        }
        private List<TEntity> Get()
        {
            List<TEntity> result = new List<TEntity>();
            if (db.ContainsKey(EntityName))
                result = db[EntityName];

            return result;
        }


        private string EntityName
        {
            get
            {
                return typeof(TEntity).Name + "s";
            }
        }

        // void Delete<T>(T item)
        // {
        //     _db.GetCollection<T>().DeleteOne(T);
        // }

        public Task<TEntity> Single(Expression<Func<TEntity, bool>> expression)
        {
            //var filter = Builders<TEntity>.Filter.Eq("Name","Hello Naveen");
            return Task.Run(() =>
            {
                return Get().AsQueryable().Where(expression).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<TEntity>> All(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return Get().AsQueryable().Where(expression).ToList();
            });

        }
        public IQueryable<TEntity> All()
        {
            return Get().AsQueryable();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return All().ToList();
            });
        }

        //System.Linq.IQueryable<T> All<T>(int page, int pageSize);
        public async Task<TEntity> AddAsync(TEntity item)// where T : class, new()
        {
            await Task.Run(() =>
            {

                var res = Get();
                if (db.ContainsKey(EntityName))
                    db.Remove(EntityName);
                res.Add(item);
                db.Add(EntityName, res);
            });
            return item;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            await Task.Run(() =>
            {

                var res = Get();
                if (db.ContainsKey(EntityName))
                    db.Remove(EntityName);

                var find = res.AsQueryable().Where(expression).FirstOrDefault();
                res.Remove(find);
                db.Add(EntityName, res);
            });
            //return item;
            //await Get().DeleteOneAsync(expression);
        }
        public async Task DeleteAllAsync()
        {
            await Task.Run(() =>
            {
                if (db.ContainsKey(EntityName))
                    db.Remove(EntityName);
            });
        }

        public async Task UpdateAsync(TEntity item)
        {
           await UpdateAsync(i=> i.Id== item.Id,item);
        }

        public async Task  UpdateAsync(Expression<Func<TEntity, bool>> expression,TEntity item)
        {
            await Task.Run(() =>
            {

                var res = Get();
                if (db.ContainsKey(EntityName))
                    db.Remove(EntityName);
                var find = res.AsQueryable().Where(expression).FirstOrDefault();                
                db.Add(EntityName, res);
            });
        }

        public Task AddSubDocument<TFeild,TItem>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TFeild>> action ,TItem value)
        {
            return Task.Run(()=>{
                
            });
        }
    }
}