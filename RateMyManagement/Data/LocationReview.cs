using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateMyManagement.Data
{
    public class LocationReview
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public int Stars { get; set; }
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public string SentDateAndTime { get; set; } = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
        public ReviewType Type { get; set; }
        public ManagerType ManagerType;
        public List<ManagerAttribute> ManagerAttributes { get; set; } = new();
        public string ManagerName { get; set; }
    }
}
