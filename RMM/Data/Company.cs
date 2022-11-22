using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RMM.Data;

namespace RateMyManagement.Data
{
    public class Company
    {
        public static readonly string DefaultUrl = "https://i.ibb.co/WkG4Jgf/test.png";
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public string Industry { get; set; }
        public string LogoUrl { get; set; }
        public string LogoDeleteUrl { get; set; }
        public List<string> LocationIds { get; set; } = new List<string>();
        public List<Review> CompanyReviews { get; set; } = new List<Review>();
        public string GetLogoUrl()
        {
            return string.IsNullOrWhiteSpace(LogoUrl) ? DefaultUrl : LogoUrl;
        }
    }
}
