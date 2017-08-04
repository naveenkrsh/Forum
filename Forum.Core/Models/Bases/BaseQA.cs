using System;
namespace Forum.Core.Models.Bases
{
    public class BaseQA
    {
        public BaseQA()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
        public Guid Id {get;set;}
        public string Value{get;set;}
        public string UserId{get;set;}
        public DateTime CreatedDate{get;set;}
        public DateTime ModifiedDate{get;set;}
    }
}