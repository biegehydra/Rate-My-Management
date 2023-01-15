using Microsoft.EntityFrameworkCore;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Data;
using RateMyManagement.IServices;

namespace RateMyManagement.Services;

public class SqlLocationService : ILocationService
{
    private ApplicationDbContext _dbContext;
    public SqlLocationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveLocationAsync(Location location)
    {
        var result = await _dbContext.Locations.FindAsync(location.Id);
        if (result != null)
        {
            _dbContext.Locations.Update(location);
        }
        else
        {
            await _dbContext.Locations.AddAsync(location);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateLocationsAsync(IEnumerable<Location> locations)
    {
        await _dbContext.Locations.AddRangeAsync(locations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Location> GetLocationAsync(string locationId)
    {
        var result = await _dbContext.Locations
            .Include(x => x.LocatioReviews)
            .Include(x => x.Company)
            .FirstOrDefaultAsync(x => x.Id == locationId);
        return result ?? Location.Default;
    }

    public async Task<List<Location>> GetLocationsAsync(IEnumerable<string> locationIds)
    {
        return await _dbContext.Locations
            .Include(x => x.LocatioReviews)
            .Include(x => x.Company)
            .Where(x => locationIds.Contains(x.Id)).ToListAsync();
    }

    public async Task AddLocationReviewAsync(string locationId, LocationReview review)
    {
        await _dbContext.LocationReviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddLocationReviewsAsync(string locationId, IEnumerable<LocationReview> reviews)
    {
        await _dbContext.LocationReviews.AddRangeAsync(reviews);
        await _dbContext.SaveChangesAsync();
    }
}