using Forum.Test.Setup;
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
using Xunit.Abstractions;
namespace Forum.Test
{
    [Collection("Database collection")]
    public class TagServiceTest
    {
        IService<Tag> _tagService;
        private readonly ITestOutputHelper output;
        // public TagServiceTest()
        // {
        //     IUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
        //     _tagService = new TagService(_unitOfWork);
        // }
        public TagServiceTest(DatabaseFixture fixture,ITestOutputHelper output)
        {
            IUnitOfWork _unitOfWork = fixture.Db;
            _tagService = new TagService(_unitOfWork);
             this.output = output;
        }

        [Fact]
        public async Task Delete_Add_Find_Test()
        {


            var tag = new Tag("CSharp");
            tag.Id = "5977844c30656d5f7b1b1364";
            //var res = _tagService.SaveAsync(tag);

            await _tagService.DeleteAsync(y => y.Id == tag.Id);
            await _tagService.SaveAsync(tag);
            var newTag =  await _tagService.FindOneAsync(x => x.Name == "CSharp");
     
            Assert.NotNull(newTag);
            Assert.Equal(newTag.Id, tag.Id);

            var temp = "my class!";
            output.WriteLine("This is output from {0}", temp);
        }

        [Fact]
        public void AddDuplicateTest()
        {
            var tag = new Tag("CSharp");

            try
            {
                Add().Wait();
                _tagService.SaveAsync(tag).Wait(); ;
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

            await Add();
            var result = await _tagService.GetAllAsync();
            bool res = result.Count() > 0 ? true : false;
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