using Bogus.DataSets;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RateMyManagement.IServices;
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
        public async Task<float> GetRating(IMongoService mongoService)
        {
            var test = await mongoService.TryGetLocationsAsync(LocationIds);
            if (test.Item1)
            {
                if (test.Item2.Count == 0)
                {
                    return 0;
                }
                return test.Item2.Average(x => x.GetRating());
            }
            return 0;
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
