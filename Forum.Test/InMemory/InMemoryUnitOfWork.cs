using Forum.Core;
using Forum.Core.Models;
using Forum.Core.Models.User;
using Forum.Infrastructure.Contract;
using Models = Forum.Core.Models;
using System.Collections.Generic;
namespace Forum.Test.InMemory
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private IRepository<Models.Tag> _tagRepo;
        private IRepository<User> _userRepo;

        private IRepository<Question> _questionRepo;
        public InMemoryUnitOfWork()
        {
            //Seed();
        }

        public IRepository<Models.Tag> TagRepository
        {
            get
            {
                if (_tagRepo == null)
                    _tagRepo = new InMemoryRepository<Models.Tag>();

                return _tagRepo;
            }
        }

        public IRepository<User> UserReository
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new InMemoryRepository<User>();

                return _userRepo;
            }
        }

        public IRepository<Question> QuestionReposity
        {
            get
            {
                if (_questionRepo == null)
                    _questionRepo = new InMemoryRepository<Question>();

                return _questionRepo;
            }
        }

        private void Seed()
        {

            var user = new Models.User.User();
            user.Email = "naveen.kr.sh1993@gmail.com";
            user.Password = user.Hash("naveen");
            user.Id = "597638e11ff56e7824742935";
            user.ProfileDetails.FirstName = "Naveen";
            user.ProfileDetails.LastName = "Sharma";

            var task = UserReository.DeleteAsync(x => x.Id == "597638e11ff56e7824742935");
            task.ContinueWith((t) => { UserReository.AddAsync(user); }).Wait();


        }
    }
}