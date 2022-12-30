using Bogus.DataSets;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RateMyManagement.IServices;
using RateMyManagement.Data;

namespace RateMyManagement.Data
{
    public class Company
    {
        public static readonly Company Default = new Company()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Description = "DESCRIPTION",
            Industry = "INDUSTRY",
            LocationIds = new List<string>(),
            LogoDeleteUrl = string.Empty,
            LogoUrl = string.Empty,
            Name = "DEFAULT",
            Rating = 0
        };
        public static readonly string DefaultUrl = "https://i.ibb.co/WkG4Jgf/test.png";
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public async Task<float> GetRating(ILocationService locationService)
        {
            var locations = await locationService.GetLocationsAsync(LocationIds);
            if (locations.Count == 0) return 0;
            return locations.Average(x => x.GetRating());
        }
        public string Industry { get; set; }
        public string LogoUrl { get; set; }
        public string LogoDeleteUrl { get; set; }
        public List<string> LocationIds { get; set; } = new List<string>();
        public string GetLogoUrl()
        {
            return string.IsNullOrWhiteSpace(LogoUrl) ? DefaultUrl : LogoUrl;
        }
    }
}
