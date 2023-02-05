using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("TB_ORDERS");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_ORDER");
            builder.Property(x => x.Date).HasColumnName("DATE").IsRequired();
            builder.Property(x => x.Status).HasColumnName("STATUS").HasConversion<string>().IsRequired();

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.Id);

        }
    }
}
