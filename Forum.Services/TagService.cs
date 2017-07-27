using Forum.Core;
using Forum.Core.Models;
using Forum.Services.Contract;
using Forum.Infrastructure.Contract;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Forum.Services
{
    public class TagService : ITagService
    {
        private IRepository<Tag> _tagRepo;
        public TagService(IUnitOfWork unitOfwork)
        {
            _tagRepo = unitOfwork.TagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _tagRepo.GetAllAsync();
        }
        public async Task<Tag> FindOneAsync(Expression<Func<Tag, bool>> expression)
        {

            var res = await _tagRepo.Single(expression);
            return res;
        }
        public  Task<Tag> SaveAsync(Tag tag)
        {
             return FindOneAsync(t=> t.Name == tag.Name)
             .ContinueWith((t)=>{
                 if(t.Result != null)
                  {
                    throw new Exception("Duplicate tag entry");
                  }
                  return  _tagRepo.AddAsync(tag);
             }).Result;
  
        }
        public Task DeleteAsync(Expression<Func<Tag, bool>> expression)
        {
            return _tagRepo.DeleteAsync(expression);
        }
    }
}