using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RMM.Data
{
    public class Review
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public int Stars { get; set; }
        public string SenderUsername { get; set; }
        public string Content { get; set; }

        public string SentDateAndTime { get; set; } = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
    }

    public class LocationReview : Review
    {
        public ReviewType Type { get; set; }
        public ManagerType ManagerType;
        public string ManagerName { get; set; }
    }
    public enum ReviewType
    {
        Null,
        Customer,
        Employee
    }

    public enum ManagerType
    {
        Null,
        HumanResources,
        GeneralManager
    }
    public enum ManagerAttributes
    {
        Null,
        Rude,
        Condescending,
        Helpful,
        Understanding,

    }
}
