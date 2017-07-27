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

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            return  _tagRepo.GetAllAsync();
        }
        public Task<Tag> FindOneAsync(Expression<Func<Tag, bool>> expression)
        {

            return _tagRepo.Single(expression);
            //return res;
        }
        public async Task<Tag> SaveAsync(Tag tag)
        {
            var task = await FindOneAsync(t => t.Name == tag.Name);
            if (task != null)
            {
                throw new Exception("Duplicate tag entry");
            }
            return await _tagRepo.AddAsync(tag);
        }
        public Task DeleteAsync(Expression<Func<Tag, bool>> expression)
        {
            return _tagRepo.DeleteAsync(expression);
        }
    }
}