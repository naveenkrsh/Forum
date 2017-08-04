using Forum.Core;
using Forum.Core.Models.Bases;
namespace Forum.Infrastructure.Contract
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}