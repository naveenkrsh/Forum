using Forum.Core.Models.Bases;
using System;
using System.Collections.Generic;
namespace Forum.Core.Models
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Answers = new List<Answer>();
            Comments = new List<Comment>();
            TagIds = new List<string>();
        }
        public string Value {get;set;}
        public List<Answer> Answers{get;set;}
        public List<Comment> Comments{get;set;}
        public List<string> TagIds {get;set;}
    }
}