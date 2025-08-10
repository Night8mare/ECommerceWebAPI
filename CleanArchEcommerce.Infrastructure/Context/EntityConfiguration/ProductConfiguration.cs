using CleanArchEcommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchEcommerce.Infrastructure.Context.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsAvailable)
                .HasMaxLength(255)
                .HasDefaultValue("InStock");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PurchasePrice).HasColumnType("money");
        }
    }
}
