using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("TB_PRODUCTS");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ID_PRODUCT");
            builder.Property(p => p.Description).HasColumnName("DESCRIPTION")
                                                .HasMaxLength(100)
                                                .IsRequired();

            builder.Property(p => p.Price).HasColumnName("PRICE")
                                                .HasPrecision(10,2)
                                                .IsRequired();
            
            builder.Property(p => p.Active).HasColumnName("ACTIVE")
                                            .IsRequired();

            
            builder.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId);                   

        }
    }
}