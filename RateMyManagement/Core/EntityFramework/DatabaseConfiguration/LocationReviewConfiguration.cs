using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagement.Data;

namespace RateMyManagement.Core.EntityFramework.DatabaseConfiguration
{
    public class LocationReviewConfiguration : IEntityTypeConfiguration<LocationReview>
    {
        public void Configure(EntityTypeBuilder<LocationReview> builder)
        {
            builder.ToTable("LocationReviews");
            builder.HasOne(x => x.Location)
                .WithMany(x => x.LocatioReviews);
        }
    }
}
