using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
    {
        public void Configure(EntityTypeBuilder<SaleEntity> builder)
        {
            builder.ToTable("TB_SALES");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_SALE");
            builder.Property(x => x.Moment).HasColumnName("MOMENT").IsRequired();
            builder.Property(x => x.SellerId).HasColumnName("ID_SELLER").IsRequired();
            builder.Property(x => x.OrderId).HasColumnName("ID_ORDER").IsRequired();
            builder.Property(x => x.Status).HasColumnName("STATUS").HasConversion<string>().IsRequired();

            builder.HasOne(x => x.Seller)
                .WithMany()
                .HasForeignKey(x => x.SellerId);

            builder.HasOne(x => x.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId);
        }
    }
}
