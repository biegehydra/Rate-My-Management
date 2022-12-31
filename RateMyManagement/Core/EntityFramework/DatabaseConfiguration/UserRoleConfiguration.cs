using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RateMyManagement.Core.EntityFramework;

namespace RateMyManagement.Core.EntityFramework.DatabaseConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "631418d7-ad65-4b34-9d28-14901dc0c897",
                    UserId = "37875bc7-588f-44f0-8419-9cbfc362e79e"
                }
            );
        }
    }
}
