using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class SellerConfiguration : IEntityTypeConfiguration<SellerEntity>
    {
        public void Configure(EntityTypeBuilder<SellerEntity> builder)
        {
            builder.ToTable("TB_SELLERS");
            
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID_SELLER");
            builder.Property(e => e.Name).HasColumnName("NAME").HasMaxLength(400).IsRequired();
            builder.Property(e => e.Email).HasColumnName("DS_EMAIL").HasMaxLength(400).IsRequired();

            builder.HasMany(x => x.Phones)
                .WithOne(s => s.SellerEntity);
        }
    }
}
