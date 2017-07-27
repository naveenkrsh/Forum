using System;
namespace Forum.Core.Models
{
    public class Voter
    {
        public bool IsUpVote{get;set;}

        public string VoterId{get;set;}

        public DateTime VotedTime{get;set;}
    }
}