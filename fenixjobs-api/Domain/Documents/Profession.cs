using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fenixjobs_api.Domain.Documents
{
    public class Profession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Experience")]
        public string Experience { get; set; }

        [BsonElement("Evidence")]
        public string Evidence { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
