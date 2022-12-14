// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment_api.Infra.Data.Context;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221118171500_add table Products")]
    partial class addtableProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("Payment_api.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_CATEGORY");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("DESCRIPTION");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("CATEGORIES", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_PRODUCT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("DESCRIPTION");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT")
                        .HasColumnName("PRICE");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("PRODUCTS", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.ProductEntity", b =>
                {
                    b.HasOne("Payment_api.Domain.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
