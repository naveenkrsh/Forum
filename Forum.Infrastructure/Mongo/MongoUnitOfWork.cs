using Forum.Core;
using Forum.Core.Models;
using Forum.Core.Models.User;
using Forum.Infrastructure.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Models = Forum.Core.Models;

namespace Forum.Infrastructure.Mongo
{
    public class MongoUnitOfWork : IUnitOfWork
    {

        private IMongoDatabase _db;
        private IRepository<Models.Tag> _tagRepo;
        private IRepository<User> _userRepo;

        private IRepository<Question> _questionRepo;
        public MongoUnitOfWork()
        {
            MongoClient _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("questions");
            Seed();
        }

        public IRepository<Models.Tag> TagRepository
        {
            get
            {
                if (_tagRepo == null)
                    _tagRepo = new MongoRepository<Models.Tag>(_db);

                return _tagRepo;
            }
        }

        public IRepository<User> UserReository
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new MongoRepository<User>(_db);

                return _userRepo;
            }
        }

        public IRepository<Question> QuestionReposity
        {
            get
            {
                if (_questionRepo == null)
                    _questionRepo = new MongoRepository<Question>(_db);

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