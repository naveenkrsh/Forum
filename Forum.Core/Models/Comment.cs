using Forum.Core.Models.Bases;
using System.Collections.Generic;
namespace Forum.Core.Models
{
    public class Comment : BaseQA
    {
        public List<Comment> ReComment{get;set;}
    }
}