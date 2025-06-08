using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MongoModels
{
    public class MongoTaskList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Tasks { get; set; } = new List<string>();

        [BsonElement("ownerUserId")]
        public string OwnerUserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> AllowedUserIds { get; set; } = new List<string>();
    }
}
