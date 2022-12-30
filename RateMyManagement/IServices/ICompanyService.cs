using RateMyManagement.Data;

namespace RateMyManagement.IServices;

public interface ICompanyService
{
    public Task SaveCompanyAsync(Company company);
    public Task CreateCompaniesAsync(IEnumerable<Company> company);
    public Task<Company> GetCompanyAsync(string companyId);
    public Task<List<Company>> GetCompaniesAsync(IEnumerable<string> companyIds);
    public Task AddLocationIdToCompanyAsync(string companyId, string locationId);
    public Task AddLocationIdsToCompanyAsync(string companyId, IEnumerable<string> locationId);
    public Task<List<Company>> GetAllCompaniesAsync();
    public Task<List<Company>> QueryCompaniesByStartingLetter(char letter);
}