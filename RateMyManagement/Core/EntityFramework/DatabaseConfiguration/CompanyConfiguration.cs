using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagement.Data;

namespace RateMyManagement.Core.EntityFramework.DatabaseConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasMany(x => x.Locations)
                .WithOne(x => x.Company);
        }
    }
}