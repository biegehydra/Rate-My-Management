using MongoDB.Driver;
using RateMyManagement.Data;
using RateMyManagement.IServices;
using RMM.Data;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using ZstdSharp.Unsafe;

namespace RateMyManagement.Services
{
    public class MongoService : IMongoService
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Company> _companyTable;
        private IMongoCollection<Location> _locationTable;
        public MongoService(string databaseName, string collectionName)
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _database = _mongoClient.GetDatabase(databaseName);
            _companyTable = _database.GetCollection<Company>(collectionName);
            _locationTable = _database.GetCollection<Location>("Location");
        }
        #region Location
        public async Task CreateLocationAsync(Location location)
        {
            var cursor = await _locationTable.FindAsync(x => x.Id == location.Id);
            var locationObj = await cursor.FirstOrDefaultAsync();
            if (locationObj == null)
            {
                await _locationTable.InsertOneAsync(location);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public async Task<(bool, Location?)> TryGetLocationAsync(string locationId)
        {
            var cursor = await _locationTable.FindAsync(x => x.Id == locationId);
            var location = await cursor.FirstOrDefaultAsync();
            if (location != null) return (true, location);
            return (false, default);
        }

        public async Task UpdateLocationDetailsAsync(string id, string address, string city)
        {
            var filter = Builders<Location>.Filter.Eq(x => x.Id, id);
            var update = Builders<Location>.Update.Set(x => x.Address, address).Set(x => x.City, city);
            await _locationTable.UpdateOneAsync(filter, update);
        }
        public async Task AddLocationReviewAsync(string locationId, LocationReview review)
        {
            var filter = Builders<Location>.Filter.Eq(x => x.Id, locationId);
            var update = Builders<Location>.Update.Push(x => x.LocatioReviews, review);
            await _locationTable.UpdateOneAsync(filter, update);
        }

        #endregion

        #region Company
        public async Task CreateCompanyAsync(Company company)
        {
            var cursor = await _companyTable.FindAsync(x => x.Id == company.Id);
            var companyObj = await cursor.FirstOrDefaultAsync();
            if (companyObj == null)
            {
                await _companyTable.InsertOneAsync(company);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public async Task<(bool, Company?)> TryGetCompanyAsync(string companyId)
        {
            var cursor = await _companyTable.FindAsync(x => x.Id == companyId);
            var company = await cursor.FirstOrDefaultAsync();
            if (company != null) return (true, company);
            return (false, default);
        }
        public async Task AddLocationIdToCompanyAsync(string companyId, string locationId)
        {
            var filter = Builders<Company>.Filter.Eq(x => x.Id, companyId);
            var update = Builders<Company>.Update.Push(x => x.LocationIds, locationId);
            await _companyTable.UpdateOneAsync(filter, update);
        }
        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            var result = await _companyTable.FindAsync(x => true);
            return await result.ToListAsync();
        }

        public async Task UpdateCompanyDetailsAsync(string id, string name, string industry, string description)
        {
            var filter = Builders<Company>.Filter.Eq(company => company.Id, id);
            var update = Builders<Company>.Update
                .Set(company => company.Name, name)
                .Set(company => company.Industry, industry)
                .Set(company => company.Description, description);
            await _companyTable.UpdateOneAsync(filter, update);
        }
        public async Task<List<Company>> QueryCompaniesByStartingLetter(char letter)
        {
            var filter = Builders<Company>.Filter.Regex(x => x.Name, new BsonRegularExpression(new Regex($"(?i)^" + letter, RegexOptions.IgnoreCase)));
            var companies = await _companyTable.FindAsync(filter);
            var test = await companies.ToListAsync();
            return test;
        }

        #endregion
    }
}
