using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using RateMyManagement.Data;
using RateMyManagement.IServices;

namespace RateMyManagement.Services;

public class MongoCompanyService : ICompanyService
{
    private MongoClient _mongoClient;
    private IMongoDatabase _database;
    private IMongoCollection<Company> _companyTable;
    private IMongoCollection<Location> _locationTable;
    public MongoCompanyService(string databaseName, string companyCollectionName, string locationCollectionName)
    {
        _mongoClient = new MongoClient("mongodb://localhost:27017");
        _database = _mongoClient.GetDatabase(databaseName);
        _companyTable = _database.GetCollection<Company>(companyCollectionName);
        _locationTable = _database.GetCollection<Location>(locationCollectionName);
    }
    public async Task SaveCompanyAsync(Company company)
    {
        var cursor = await _companyTable.FindAsync(x => x.Id == company.Id);
        var companyObj = await cursor.FirstOrDefaultAsync();
        if (companyObj == null)
        {
            await _companyTable.InsertOneAsync(company);
        }
        else
        {
            await _companyTable.ReplaceOneAsync(x => x.Id == company.Id, company);
        }
    }
    public async Task CreateCompaniesAsync(IEnumerable<Company> companies)
    {
        await _companyTable.InsertManyAsync(companies);
    }
    public async Task<Company> GetCompanyAsync(string companyId)
    {
        var cursor = await _companyTable.FindAsync(x => x.Id == companyId);
        var company = await cursor.FirstOrDefaultAsync();
        return company ?? new Company();
    }
    public async Task<List<Company>> GetCompaniesAsync(IEnumerable<string> companyIds)
    {
        var filter = Builders<Company>.Filter.In(x => x.Id, companyIds);
        var cursor = await _companyTable.FindAsync(filter);
        var companies = await cursor.ToListAsync();
        return companies ?? new List<Company>();
    }
    public async Task AddLocationIdToCompanyAsync(string companyId, string locationId)
    {
        var filter = Builders<Company>.Filter.Eq(x => x.Id, companyId);
        var update = Builders<Company>.Update.Push(x => x.LocationIds, locationId);
        await _companyTable.UpdateOneAsync(filter, update);
    }
    public async Task AddLocationIdsToCompanyAsync(string companyId, IEnumerable<string> locationIds)
    {
        var filter = Builders<Company>.Filter.Eq(x => x.Id, companyId);
        var update = Builders<Company>.Update.PushEach(x => x.LocationIds, locationIds);
        await _companyTable.UpdateOneAsync(filter, update);
    }
    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        var result = await _companyTable.FindAsync(x => true);
        return await result.ToListAsync();
    }
    public async Task<List<Company>> QueryCompaniesByStartingLetter(char letter)
    {
        var filter = Builders<Company>.Filter.Regex(x => x.Name, new BsonRegularExpression(new Regex($"(?i)^" + letter, RegexOptions.IgnoreCase)));
        var companies = await _companyTable.FindAsync(filter);
        var test = await companies.ToListAsync();
        return test;
    }
}