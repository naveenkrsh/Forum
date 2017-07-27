using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
namespace Forum.Core.Models.Bases
{
    public abstract class BaseEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] 
        public string Id{get;set;}

        public BaseEntity()
        {
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = DateTime.UtcNow;
        }
        public DateTime CreatedTime{get;set;}

        public DateTime ModifiedTime{get;set;}

        public long CreatedBy{get;set;}

        public long ModifiedBy{get;set;}

        public override string ToString()
        {
            return "Id : "+ Id;
        }
    }
}