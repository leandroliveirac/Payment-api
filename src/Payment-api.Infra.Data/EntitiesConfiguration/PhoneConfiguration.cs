using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class PhoneConfiguration : IEntityTypeConfiguration<PhoneEntity>
    {
        public void Configure(EntityTypeBuilder<PhoneEntity> builder)
        {
            builder.ToTable("TB_PHONES");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_PHONE");
            builder.Property(x => x.Ddd).HasColumnName("DDD").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Number).HasColumnName("NUMBER").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Type).HasColumnName("TYPE").HasConversion<string>().IsRequired();
            builder.Property(x => x.SellerId).HasColumnName("ID_SELLER").IsRequired();

            builder.HasOne(x => x.SellerEntity)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(x => x.SellerId);

        }
    }
}
