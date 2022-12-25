using MongoDB.Driver;
using RateMyManagement.Data;
using RateMyManagement.IServices;

namespace RateMyManagement.Services;

public class MongoLocationService : ILocationService
{
    private MongoClient _mongoClient;
    private IMongoDatabase _database;
    private IMongoCollection<Company> _companyTable;
    private IMongoCollection<Location> _locationTable;
    public MongoLocationService(string databaseName, string companyCollectionName, string locationCollectionName)
    {
        _mongoClient = new MongoClient("mongodb://localhost:27017");
        _database = _mongoClient.GetDatabase(databaseName);
        _companyTable = _database.GetCollection<Company>(companyCollectionName);
        _locationTable = _database.GetCollection<Location>(locationCollectionName);
    }
    public async Task SaveLocationAsync(Location location)
    {
        var cursor = await _locationTable.FindAsync(x => x.Id == location.Id);
        var locationObj = await cursor.FirstOrDefaultAsync();
        if (locationObj == null)
        {
            await _locationTable.InsertOneAsync(location);
        }
        else
        {
            await _locationTable.ReplaceOneAsync(x => x.Id == location.Id, location);
        }
    }
    public async Task CreateLocationsAsync(IEnumerable<Location> locations)
    {
        await _locationTable.InsertManyAsync(locations);
    }
    public async Task<Location> GetLocationAsync(string locationId)
    {
        var cursor = await _locationTable.FindAsync(x => x.Id == locationId);
        var location = await cursor.FirstOrDefaultAsync();
        return location ?? new Location();
    }
    public async Task<List<Location>> GetLocationsAsync(IEnumerable<string> locationIds)
    {
        var filter = Builders<Location>.Filter.In(x => x.Id, locationIds);
        var cursor = await _locationTable.FindAsync(filter);
        var locations = await cursor.ToListAsync();
        return locations ?? new List<Location>();
    }
    public async Task AddLocationReviewAsync(string locationId, LocationReview review)
    {
        var filter = Builders<Location>.Filter.Eq(x => x.Id, locationId);
        var update = Builders<Location>.Update.Push(x => x.LocatioReviews, review);
        await _locationTable.UpdateOneAsync(filter, update);
    }
    public async Task AddLocationReviewsAsync(string locationId, IEnumerable<LocationReview> reviews)
    {
        var filter = Builders<Location>.Filter.Eq(x => x.Id, locationId);
        var update = Builders<Location>.Update.PushEach(x => x.LocatioReviews, reviews);
        await _locationTable.UpdateOneAsync(filter, update);
    }
}