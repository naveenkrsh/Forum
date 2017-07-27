using Forum.Core.Models.Bases;
using System;
using System.Collections.Generic;
namespace Forum.Core.Models
{
    public class Answer : BaseQA
    {
        public Vote VoteCount{get;set;}
        public List<Comment> Comments{get;set;}
        public List<Voter> Voters{get;set;}
       
    }
}