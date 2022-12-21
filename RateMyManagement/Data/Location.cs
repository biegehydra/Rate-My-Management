using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RMM.Data;

namespace RateMyManagement.Data
{
    public class Location
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string CompanyId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public float GetRating()
        {
            if (LocatioReviews.Count == 0) return 0;
            return (float)LocatioReviews.Select(x => x.Stars).Average();
        }
        public List<LocationReview> LocatioReviews { get; set; } = new List<LocationReview>();
    }
}
