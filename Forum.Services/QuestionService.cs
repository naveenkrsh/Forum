using Forum.Services.Contract;
using Forum.Core.Models;
using Forum.Core;
using Forum.Infrastructure.Contract;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Forum.Services
{
    public class QuestionService : IQuestionService
    {
        private IRepository<Question> _quesRepo;
        public QuestionService(IUnitOfWork unitOfwork)
        {
            _quesRepo = unitOfwork.GetRepository<Question>();
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            return  _quesRepo.GetAllAsync();
        }
        public Task<Question> FindOneAsync(Expression<Func<Question, bool>> expression)
        {

            return _quesRepo.Single(expression);
            //return res;
        }
        public async Task<Question> SaveAsync(Question question)
        {
            var task = await FindOneAsync(t => t.Value == question.Value);
            if (task != null)
            {
                throw new Exception("Duplicate Question entry");
            }
            return await _quesRepo.AddAsync(question);
        }
        public Task DeleteAsync(Expression<Func<Question, bool>> expression)
        {
            return _quesRepo.DeleteAsync(expression);
        }

        public async void UpdateAsync(Question question)
        {
            question.ModifiedTime = new DateTime().ToUniversalTime();
            await _quesRepo.UpdateAsync(question);
        }

        public async Task<Answer> AddAnswer(string id,Answer ans)
        {
            //return null;
            await _quesRepo.AddSubDocument<List<Answer>,Answer>(x=>x.Id==id,(x)=>x.Answers,ans);
            return ans;
        }
        
    }
}