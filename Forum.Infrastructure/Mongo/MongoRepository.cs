using Forum.Core;
using Forum.Core.Models;
using Forum.Core.Models.Bases;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
namespace Forum.Infrastructure.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        //MongoServer _server;
        IMongoDatabase _db;

        public MongoRepository(IMongoDatabase db)
        {
            _db = db;
        }
        private IMongoCollection<TEntity> Get()
        {

            var result = _db.GetCollection<TEntity>(typeof(TEntity).Name + "s");

            return result;
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
            await Get().InsertOneAsync(item);
            return item;
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
             return Get().DeleteOneAsync(expression);
        }
        public Task DeleteAllAsync()
        {
            return Get().DeleteManyAsync(Builders<TEntity>.Filter.Empty);
        }

        public async Task UpdateAsync(TEntity item)
        {
            await UpdateAsync((i=> i.Id == item.Id), item);
        }
        public async Task UpdateAsync(Expression<Func<TEntity, bool>> expression,TEntity item)
        {
            await Get().FindOneAndReplaceAsync(expression,item);
        }

        public async Task AddSubDocument<TFeild,TItem>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TFeild>> action ,TItem value)
        {
            //var source = Expression.Parameter(typeof(TEntity), "source");
          
            FieldDefinition<TEntity> fd = GetFiledName(action);
            //Func<string> lengthMethod = fie.Compile();
            //string stringLength = lengthMethod();
            //var update = Builders<TEntity>.Update.Push(field,value);
            var update = Builders<TEntity>.Update.Push(fd,value);
            await Get().UpdateOneAsync(expression,update); 
        }

        private static string GetFiledName<TFeild>(Expression<Func<TEntity, TFeild>> action)
        {
            var expression = (MemberExpression) action.Body;
            string name = expression.Member.Name;
            return name;
        }
    }
}