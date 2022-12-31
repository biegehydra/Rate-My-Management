using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagement.Data;

namespace RateMyManagement.Core.EntityFramework.DatabaseConfiguration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");
            builder.HasOne(x => x.Company)
                .WithMany(x => x.Locations);
            builder.HasMany(x => x.LocatioReviews)
                .WithOne(x => x.Location);

        }
    }
}