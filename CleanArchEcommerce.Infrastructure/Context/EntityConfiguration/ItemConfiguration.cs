using CleanArchEcommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchEcommerce.Infrastructure.Context.EntityConfiguration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_Cart");

            entity.ToTable("Item");

            entity.Property(e => e.ItemStatus)
                .HasMaxLength(255)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Cart).WithMany(p => p.Items)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_CartId");

            entity.HasOne(d => d.Order).WithMany(p => p.Items)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Order_Item");

            entity.HasOne(d => d.Product).WithMany(p => p.Items)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Item");
        }
    }
}
