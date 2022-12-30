using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RateMyManagement.Data;

namespace RateMyManagement.Core.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(1000)
                    .IsRequired();
                entity.Property(e => e.Industry)
                    .HasColumnName("Industry")
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(e => e.LogoUrl)
                    .HasColumnName("LogoUrl")
                    .HasMaxLength(200)
                    .IsRequired(false);
                entity.Property(e => e.LocationIds)
                    .HasColumnName("LocationIds")
                    .HasMaxLength(10000)
                    .IsRequired(false)
                    .HasDefaultValue(new List<string>());
                entity.Property(e => e.LogoDeleteUrl)
                    .HasColumnName("LogoDeleteUrl")
                    .HasDefaultValue(string.Empty)
                    .HasMaxLength(200)
                    .IsRequired(false);
                entity.Property(e => e.Rating)
                    .HasColumnName("Rating")
                    .HasDefaultValue(0)
                    .IsRequired(false);
                entity.HasKey(e => e.Id);
            });
        }
    }
}