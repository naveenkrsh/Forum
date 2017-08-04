using Forum.Core;
using Forum.Core.Models.Bases;
using Forum.Infrastructure.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Models = Forum.Core.Models;
using System.Collections.Generic;
namespace Forum.Infrastructure.Mongo
{
    public class MongoUnitOfWork : IUnitOfWork
    {

        private IMongoDatabase _db;
        private Dictionary<string, object> _repoList;
       

        
        public MongoUnitOfWork()
        {
            MongoClient _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("questions");
           _repoList = new Dictionary<string, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (_repoList.ContainsKey(key))
            {
                var _repo = _repoList[key]; //new InMemoryRepository<TEntity>();

                if (_repo == null)
                {
                    _repoList[key] = new MongoRepository<TEntity>(_db);
                }
            }
            else
            {
                   _repoList[key] = new MongoRepository<TEntity>(_db);
            }
            return (MongoRepository<TEntity>)_repoList[key];
        }

        // private void Seed()
        // {

        //     var user = new Models.User.User();
        //     user.Email = "naveen.kr.sh1993@gmail.com";
        //     user.Password = user.Hash("naveen");
        //     user.Id = "597638e11ff56e7824742935";
        //     user.ProfileDetails.FirstName = "Naveen";
        //     user.ProfileDetails.LastName = "Sharma";

        //     var task = UserReository.DeleteAsync(x => x.Id == "597638e11ff56e7824742935");
        //     task.ContinueWith((t) => { UserReository.AddAsync(user); }).Wait();


        // }
    }
}