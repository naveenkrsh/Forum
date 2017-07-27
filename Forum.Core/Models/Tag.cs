using Forum.Core.Models.Bases;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Forum.Core.Models
{
    public class Tag : BaseEntity
    {
        public Tag()
        {

        }

        public Tag(string name)
        {
            this.Name = name;
        }
        public string Name{get;set;}

        public override string ToString()
        {
            return "Name: "+ this.Name + " "+base.ToString();
        }
    }
}