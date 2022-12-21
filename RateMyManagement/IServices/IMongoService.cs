using RateMyManagement.Data;

namespace RateMyManagement.IServices
{
    public interface IMongoService
    {
        #region Company
        public Task CreateCompanyAsync(Company company);
        public Task<(bool, Company?)> TryGetCompanyAsync(string companyId);
        public Task<(bool, List<Company>?)> TryGetCompaniesAsync(IEnumerable<string> companyIds);
        public Task AddLocationIdToCompanyAsync(string companyId, string locationId);
        public Task<List<Company>> GetAllCompaniesAsync();
        public Task UpdateCompanyDetailsAsync(string id, string name, string industry, string description);
        public Task<List<Company>> QueryCompaniesByStartingLetter(char letter);
        #endregion

        #region Location
        public Task CreateLocationAsync(Location company);
        public Task<(bool, Location?)> TryGetLocationAsync(string locationId);
        public Task<(bool, List<Location>?)> TryGetLocationsAsync(IEnumerable<string> locationIds);
        public Task AddLocationReviewAsync(string locationId, LocationReview review);
        public Task UpdateLocationDetailsAsync(string id, string address, string city);
        #endregion
    }
}
