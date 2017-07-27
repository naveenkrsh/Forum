using Forum.Test.InMemory;
using Forum.Services.Contract;
using Forum.Services;
using Forum.Core.Models;
using Forum.Infrastructure.Contract;
using Forum.Infrastructure.Mongo;
using System;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
namespace Forum.Test
{
    public class TagServiceTest
    {
        IService<Tag> _tagService;

        public TagServiceTest()
        {
            IUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
            _tagService = new TagService(_unitOfWork);
        }

        [Fact]
        public async void Delete_Add_Find_Test()
        {
           
            var tag = new Tag("CSharp");
            tag.Id = "5977844c30656d5f7b1b1364";
            //var res = _tagService.SaveAsync(tag);
            var res = _tagService.DeleteAsync(y => y.Id == tag.Id);

            var newTag = await res.ContinueWith((t) =>
            {
                return _tagService.SaveAsync(tag);
            })
            .ContinueWith((t) =>
            {
                return _tagService.FindOneAsync(x => x.Name == "CSharp");
            }).Result;
            //Console.WriteLine("Tadadadad"+newTag.ToString());
            Assert.NotNull(newTag);
            Assert.Equal(newTag.Id, tag.Id);
        }

        [Fact]
        public void AddDuplicateTest()
        {
            var tag = new Tag("CSharp");

            try
            {
                 Add().Wait();
                _tagService.SaveAsync(tag).Wait();;
               
               

            }
            catch (AggregateException ex)
            {
                if (ex.InnerException.Message == "Duplicate tag entry")
                    Assert.True(true);
                else
                    Assert.True(false);
            }
        }

        [Fact]
        public async void GetAll()
        {
            
            
            var result = await Add().
            ContinueWith((t)=>{
                 return _tagService.GetAllAsync();
            }).Result;
           
            bool res = result.Count()>0?true:false;
            Assert.True(res);
        }

        private Task<Tag> Add()
        {
            var tag = new Tag("CSharp");
            tag.Id = "5977844c30656d5f7b1b1364";
            return _tagService.SaveAsync(tag);
        }
    }
}