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
    public class QuestionServiceTest
    {
        IService<Tag> _tagService;
        IService<Question> _quesService;
        public QuestionServiceTest(DatabaseFixture fixture)
        {
            IUnitOfWork _unitOfWork = fixture.Db;
            _tagService = new TagService(_unitOfWork);
            _quesService = new QuestionService(_unitOfWork);
        }


        [Fact]
        public async void Add_Questions_Test()
        {
            var ntag = new Tag("Question Test 1");
            //tag.Id = "5977844c30656d5f7b1b1364"; 
            var tag =  await _tagService.SaveAsync(ntag);

            Question ques = new Question();
            ques.Value = "What is ur C#?";
            ques.TagIds.Add(tag.Id);
            
            await _quesService.SaveAsync(ques);
            var newQuestion =  await _quesService.FindOneAsync(x => x.Value == "What is ur C#?");
     
            Assert.NotNull(newQuestion);
            Assert.Equal(newQuestion.Value, ques.Value);
            Assert.True(newQuestion.TagIds.Contains(tag.Id));
        }

        public async void Add_Questions_Tag_Test()
        {
            var ntag1 = new Tag("Question tag Test 1");
            //tag.Id = "5977844c30656d5f7b1b1364"; 
            var tag =  await _tagService.SaveAsync(ntag1);

            Question ques = new Question();
            ques.Value = "What is ur C#?";
            ques.TagIds.Add(tag.Id);
            
            await _quesService.SaveAsync(ques);
            var newQuestion =  await _quesService.FindOneAsync(x => x.Value == "What is ur C#?");
     
            var ntag2 = new Tag("Question tag Test 2");
            var tag2 =  await _tagService.SaveAsync(ntag2);
          
            newQuestion.TagIds.Add(tag2.Id);
           _quesService.UpdateAsync(newQuestion);
            
             var newQuestion2 =  await _quesService.FindOneAsync(x => x.Value == "What is ur C#?");
     
            Assert.NotNull(newQuestion2);
            Assert.Equal(newQuestion2.Value, newQuestion.Value);
            Assert.True(newQuestion2.TagIds.Count==2);
            Assert.True(newQuestion.TagIds.Contains(tag.Id));
        }
    }
}