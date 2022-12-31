using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateMyManagement.Data
{
    public class Location
    {
        public static readonly Location Default = new Location()
        {
            Id = Guid.NewGuid().ToString(),
            City = "Default City",
            Address = "Default Address",
            LocatioReviews = new List<LocationReview>(),
        };
        public string Id { get; set; }
        public Company Company { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<LocationReview> LocatioReviews { get; set; } = new();
        public float GetRating()
        {
            return LocatioReviews.Count == 0 ? 0f : (float) LocatioReviews.Average(x => x.Stars);
        }
    }
}
