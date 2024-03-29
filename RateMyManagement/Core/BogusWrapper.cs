﻿using Bogus;
using RateMyManagement.Data;

namespace RateMyManagement.Core
{
    public class BogusWrapper
    {
        public static IEnumerable<Company> GenerateFakeCompanies(int amount)
        {
            var faker = new Faker<Company>()
                .StrictMode(true)
                .RuleFor(x => x.Name, (f, s) => f.Company.CompanyName())
                .RuleFor(c => c.Description, (f, s) => f.Company.Bs())
                .RuleFor(c => c.Id, (f, s) => Guid.NewGuid().ToString())
                .RuleFor(c => c.Locations, (f, s) => new List<Location>())
                .RuleFor(c => c.Industry, (f, c) => f.Commerce.Department())
                .RuleFor(c => c.LogoUrl, (f, s) => f.Image.LoremFlickrUrl(500, 500, "business"))
                .RuleFor(c => c.LogoDeleteUrl, (f, c) => string.Empty);
            return faker.Generate(amount);
        }
        public static IEnumerable<Location> GenerateFakeLocations(Company company, int amount)
        {
            var random = new Random();
            var faker = new Faker<Location>()
                .StrictMode(true)
                .RuleFor(l => l.Id, (f, s) => Guid.NewGuid().ToString())
                .RuleFor(l => l.Company, (f, s) => company)
                .RuleFor(l => l.Address, (f, s) => f.Address.StreetAddress())
                .RuleFor(l => l.LocatioReviews, (f, s) => new List<LocationReview>())
                .RuleFor(l => l.City, (f, s) => f.Address.City());
            return faker.Generate(amount);
        }

        public static IEnumerable<LocationReview> GenerateFakeLocationReviews(Location location, int amount)
        {
            var random = new Random();
            var today = DateTime.Now;
            var thirtyDaysAgo = today.Subtract(new TimeSpan(30, 0, 0, 0));
            var faker = new Faker<LocationReview>()
                .StrictMode(true)
                .RuleFor(lr => lr.Id, (f,s) => f.Random.Guid().ToString())
                .RuleFor(lr => lr.ManagerName, (f, s) => f.Name.FullName())
                .RuleFor(lr => lr.ManagerType, (f, s) => f.PickRandom<ManagerType>().ToString())
                .RuleFor(lr => lr.SenderUsername, (f, s) => f.Internet.UserName())
                .RuleFor(lr => lr.Location, location)
                .RuleFor(lr => lr.Type,
                    (f, s) => f.PickRandom(new ReviewType[]
                        {ReviewType.Customer, ReviewType.Employee}).ToString())
                .RuleFor(lr => lr.Stars, (f, s) => random.Next(1, 6))
                .RuleFor(lr => lr.SentDateAndTime, (f, s) =>
                    f.Date.Between(thirtyDaysAgo, today).ToShortTimeString() + " " +
                    f.Date.Between(thirtyDaysAgo, today).ToShortDateString())
                .RuleFor(lr => lr.Content, (f, s) => f.Lorem.Paragraph(3))
                .RuleFor(lr => lr.ManagerAttributes,
                    (f, s) => string.Join(",", f.PickRandom(Enum.GetValues<ManagerAttribute>(), random.Next(1, Enum.GetValues<ManagerAttribute>().Length)).ToList()));
            return faker.Generate(amount);
        }
    }
}
