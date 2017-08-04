using Forum.Core;
using Forum.Core.Models;
using Forum.Core.Models.User;
using Forum.Core.Models.Bases;
using Forum.Infrastructure.Contract;
using Models = Forum.Core.Models;
using System.Collections.Generic;
namespace Forum.Test.InMemory
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repoList;
        private static object sync = new object();
        public InMemoryUnitOfWork()
        {
            _repoList = new Dictionary<string, object>();
            //Seed();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (_repoList.ContainsKey(key))
            {
                var _repo = _repoList[key]; //new InMemoryRepository<TEntity>();

                if (_repo == null)
                {
                    lock (sync)
                    {
                        _repoList[key] = new InMemoryRepository<TEntity>();
                    }
                }
            }
            else
            {
                lock (sync)
                {
                    _repoList[key] = new InMemoryRepository<TEntity>();
                }
            }
            return (InMemoryRepository<TEntity>)_repoList[key];
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