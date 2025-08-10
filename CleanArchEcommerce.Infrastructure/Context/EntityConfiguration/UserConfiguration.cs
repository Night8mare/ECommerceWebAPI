using CleanArchEcommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchEcommerce.Infrastructure.Context.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
                entity.HasKey(e => e.Id).HasName("PK_Customer");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "IX_Email").IsUnique();

                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.City).HasMaxLength(255);
                entity.Property(e => e.Country).HasMaxLength(255);
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.PhoneNo).HasMaxLength(255);
                entity.Property(e => e.PostalCard).HasMaxLength(255);
                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .HasDefaultValue("User");
                entity.Property(e => e.State).HasMaxLength(255);
                entity.Property(e => e.Token).HasDefaultValueSql("((0))");
        }
    }
}
