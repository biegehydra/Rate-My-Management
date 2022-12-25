using RateMyManagement.Data;

namespace RateMyManagement.IServices;

public interface ILocationService
{
    public Task SaveLocationAsync(Location company);
    public Task CreateLocationsAsync(IEnumerable<Location> company);
    public Task<Location> GetLocationAsync(string locationId);
    public Task<List<Location>> GetLocationsAsync(IEnumerable<string> locationIds);
    public Task AddLocationReviewAsync(string locationId, LocationReview review);
    public Task AddLocationReviewsAsync(string locationId, IEnumerable<LocationReview> review);
}