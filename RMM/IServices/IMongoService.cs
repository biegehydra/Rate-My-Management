using RateMyManagement.Data;
using RMM.Data;

namespace RateMyManagement.IServices
{
    public interface IMongoService
    {
        public  Task CreateCompanyAsync(Company company);
        public Task<(bool, Company)> TryGetCompanyAsync(string companyId);
        public Task AddCompanyReviewAsync(string companyId,Review review);
        public Task AddLocationIdToCompanyAsync(string companyId, string locationId);
        public Task<List<Company>> GetAllCompaniesAsync();
        public Task CreateLocationAsync(Location company);
        public Task<(bool, Location)> TryGetLocationAsync(string locationId);
        public Task AddLocationReviewAsync(string locationId, LocationReview review);
    }
}
