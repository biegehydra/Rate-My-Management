using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Shared.Authorization;

namespace RateMyManagement.Core.EntityFramework.DatabaseConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser()
                {
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    UserName = "Administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    PasswordHash = "AQAAAAEAACcQAAAAEE9wbiDMioih+rgxXqiZVE/w5v5F4TjX3GcO9VVj4S1kxzay0TMtB58MrZC2KoIH1g==",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                }
            );
        }
    }
}
