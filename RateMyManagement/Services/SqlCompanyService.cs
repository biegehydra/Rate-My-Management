using Microsoft.EntityFrameworkCore;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Data;
using RateMyManagement.IServices;

namespace RateMyManagement.Services;

public class SqlCompanyService : ICompanyService
{
    private ApplicationDbContext _dbContext;

    public SqlCompanyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveCompanyAsync(Company company)
    {
        var companyResult = await _dbContext.Companies.FindAsync(company.Id);
        if (companyResult != null)
        {
            _dbContext.Companies.Update(company);
        }
        else
        {
            _dbContext.Companies.Add(company);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateCompaniesAsync(IEnumerable<Company> companies)
    {
        await _dbContext.Companies.AddRangeAsync(companies);
    }

    public async Task<Company> GetCompanyAsync(string companyId)
    {
        Company? company;
        await using (_dbContext)
        {
            company = await _dbContext.Companies.Include(c => c.Locations).FirstOrDefaultAsync(x => x.Id == companyId);
        }
        return company ?? Company.Default;
    }

    public async Task<List<Company>> GetCompaniesAsync(IEnumerable<string> companyIds)
    {
        var result = await _dbContext.Companies.Include(c => c.Locations).Where(x => companyIds.Contains(x.Id)).ToListAsync();
        return result ?? new List<Company>();
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _dbContext.Companies.Include(c => c.Locations).ToListAsync();
    }

    public async Task<List<Company>> QueryCompaniesByStartingLetter(char letter)
    {
        return await _dbContext.Companies
            .Include(c => c.Locations)
            .Where(x => EF.Functions.Like(x.Name, letter + "%")).ToListAsync();
    }
}