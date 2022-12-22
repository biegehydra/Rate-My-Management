using System.Diagnostics;
using Bogus;
using Bogus.DataSets;
using MongoDB.Bson;
using Radzen.Blazor;
using RateMyManagement.Data;
using RateMyManagement.IServices;
using Company = RateMyManagement.Data.Company;

namespace RateMyManagement.Core
{
    public class BogusWrapper
    {
        public static async Task GenerateFakeCompanies(IMongoService mongoService, int count)
        {
            var faker = new Faker<Company>()
                .StrictMode(true)
                .RuleFor(x => x.Name, (f, s) => f.Company.CompanyName())
                .RuleFor(c => c.Description, (f, s) => f.Company.Bs())
                .RuleFor(c => c.Id, (f, s) => ObjectId.GenerateNewId().ToString())
                .RuleFor(c => c.LocationIds, (f, s) => new List<string>())
                .RuleFor(c => c.Industry, (f, c) => f.Commerce.Department())
                .RuleFor(c => c.LogoUrl, (f, s) => f.Image.LoremFlickrUrl(500, 500, "business"))
                .RuleFor(c => c.LogoDeleteUrl, (f, c) => string.Empty)
                .RuleFor(c => c.Rating, (f, c) => 0);
            var companies = faker.Generate(count);
            await mongoService.CreateCompaniesAsync(companies);
        }
        public static async Task GenerateFakeLocations(IMongoService mongoService, int lower, int upper)
        {
            var companies = await mongoService.GetAllCompaniesAsync();
            foreach (var company in companies)
            {
                var random = new Random();
                var faker = new Faker<Location>()
                    .StrictMode(true)
                    .RuleFor(l => l.Id, (f, s) => ObjectId.GenerateNewId().ToString())
                    .RuleFor(l => l.CompanyId, (f, s) => company.Id)
                    .RuleFor(l => l.Address, (f, s) => f.Address.StreetAddress())
                    .RuleFor(l => l.LocatioReviews, (f, s) => new List<LocationReview>())
                    .RuleFor(l => l.City, (f, s) => f.Address.City());
                var locations = faker.Generate(random.Next(lower, upper));
                var locationIds = locations.Select(x => x.Id);
                await mongoService.CreateLocationsAsync(locations);
                await mongoService.AddLocationIdsToCompanyAsync(company.Id, locationIds);
            }
        }

        public static async Task GenerateFakeLocationReviews(IMongoService mongoService, int lower, int upper)
        {
            var companies = await mongoService.GetAllCompaniesAsync();
            var random = new Random();
            var today = DateTime.Now;
            var thirtyDaysAgo = today.Subtract(new TimeSpan(30, 0, 0, 0));
            foreach (var company in companies)
            {
                foreach (var location in company.LocationIds)
                {
                    var faker = new Faker<LocationReview>()
                        .StrictMode(true)
                        .RuleFor(lr => lr.Id, ObjectId.GenerateNewId().ToString())
                        .RuleFor(lr => lr.ManagerName, (f, s) => f.Name.FullName())
                        .RuleFor(lr => lr.ManagerType, (f, s) => f.PickRandom<ManagerType>())
                        .RuleFor(lr => lr.SenderUsername, (f, s) => f.Internet.UserName())
                        .RuleFor(lr => lr.Type,
                            (f, s) => f.PickRandom(new ReviewType[]
                                {ReviewType.Customer, ReviewType.Employee}))
                        .RuleFor(lr => lr.Stars, (f, s) => random.Next(1, 6))
                        .RuleFor(lr => lr.SentDateAndTime, (f, s) =>
                            f.Date.Between(thirtyDaysAgo, today).ToShortTimeString() + " " +
                            f.Date.Between(thirtyDaysAgo, today).ToShortDateString())
                        .RuleFor(lr => lr.Content, (f, s) => f.Lorem.Paragraph(3))
                        .RuleFor(lr => lr.ManagerAttributes,
                            (f, s) => f.PickRandom(Enum.GetValues<ManagerAttribute>(), random.Next(1, Enum.GetValues<ManagerAttribute>().Length)).ToList());
                    var reviews = faker.Generate(random.Next(lower, upper));
                    await mongoService.AddLocationReviewsAsync(location, reviews);
                }
            }
        }
    }
}
