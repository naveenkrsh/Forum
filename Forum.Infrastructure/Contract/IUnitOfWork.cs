using Forum.Core;
using Forum.Core.Models;
using Forum.Core.Models.User;
namespace Forum.Infrastructure.Contract
{
    public interface IUnitOfWork
    {
        IRepository<Tag> TagRepository{get;}
        IRepository<User> UserReository{get;}
        IRepository<Question> QuestionReposity{get;}
    }
}