using Forum.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Forum.Services.Contract
{
    public interface IQuestionService : IService<Question>
    {
         //void Update(string id,List<string> tagIds);
         Task<Answer> AddAnswer(string id,Answer ans);
         //void EditAnswer(string id,Answer ans);
         
    }
}