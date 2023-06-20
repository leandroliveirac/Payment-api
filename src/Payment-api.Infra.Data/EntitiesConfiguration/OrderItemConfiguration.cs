using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.ToTable("TB_ORDER_ITEMS");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_ORDER_ITEM");
            builder.Property(x => x.OrderId).HasColumnName("ID_ORDER").IsRequired();
            builder.Property(x => x.ProductId).HasColumnName("ID_PRODUCT").IsRequired();
            builder.Property(x => x.Quantity).HasColumnName("QUANTITY").IsRequired();
            builder.Property(x => x.ProductDescription).HasColumnName("DS_PRODUCT").IsRequired();
            builder.Property(x => x.ProductUnitPrice).HasColumnName("PRODUCT_UNIT_PRICE").IsRequired();

            builder.HasOne(x => x.Order)
                    .WithMany(o => o.Items)
                    .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(x => x.ProductId);
        }
    }
}
