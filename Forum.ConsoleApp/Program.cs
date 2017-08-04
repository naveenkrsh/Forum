using Forum.Infrastructure.Contract;
using Forum.Infrastructure.Mongo;
using Forum.Services.Contract;
using Forum.Services;
using Forum.Core.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Forum.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
              IUnitOfWork _unitOfWork = new MongoUnitOfWork();
              //var _tagService = new TagService(_unitOfWork);
              var _quesService = new QuestionService(_unitOfWork);
             // var tag = new Tag("Java");
             //_tagService.SaveAsync(tag).Wait();
            // Console.WriteLine(tag.ToString());
            // Console.WriteLine("Hello Naveen World!");
            //Console.ReadKey();
            //Func<string> f = ()=> {return "Heelo";};
            // Expression<Func<Question, List<Answer>>> action = (x)=>x.Answers;

            // Console.WriteLine(GetFiledName(action));

            // var ntag1 = new Tag("Question tag Test 1");
            // //tag.Id = "5977844c30656d5f7b1b1364"; 
            // var tag =  _tagService.SaveAsync(ntag1).Result;

             //Question ques = new Question();
             //ques.Value = "What is ur C#?";
            // ques.TagIds.Add(tag.Id);
            
            //_quesService.SaveAsync(ques).Wait();
             //var newQuestion =  _quesService.FindOneAsync(x => x.Value == "What is ur C#?").Result;
            //9EEA4136-0E14-F241-86AC-DED566A24293
            var ans = new Answer();
            ans.Value = "Ans 1";
            _quesService.AddAnswer("5984c325d699ce20c1605a50",ans).Wait();
            // var ntag2 = new Tag("Question tag Test 2");
            // var tag2 =   _tagService.SaveAsync(ntag2).Result;
          
            //newQuestion.TagIds.Add(tag2.Id);
           //_quesService.UpdateAsync(newQuestion);
            
            //var newQuestion2 =  await _quesService.FindOneAsync(x => x.Value == "What is ur C#?");

            //var newQues =  _quesService.A

            Console.WriteLine("Added");
            
     
        }

        private static string GetFiledName<TEntity,TItem>(Expression<Func<TEntity, TItem>> action)
        {
            var expression = (MemberExpression) action.Body;
            string name = expression.Member.Name;
            return name;
        }
    }
}
