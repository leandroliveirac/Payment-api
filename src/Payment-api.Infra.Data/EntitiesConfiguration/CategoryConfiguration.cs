using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_api.Domain.Entities;

namespace Payment_api.Infra.Data.EntitiesConfiguration
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("TB_CATEGORIES");
            
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Description).IsUnique();

            builder.Property(c => c.Id).HasColumnName("ID_CATEGORY");
            builder.Property(c => c.Description).HasColumnName("DESCRIPTION")
                                                .HasMaxLength(100)
                                                .IsRequired();
            
        }
    }
}