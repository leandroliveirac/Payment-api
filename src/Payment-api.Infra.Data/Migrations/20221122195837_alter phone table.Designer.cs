﻿// <auto-generated />
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
    [Migration("20221122195837_alter phone table")]
    partial class alterphonetable
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

                    b.ToTable("TB_CATEGORIES", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_ORDER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("DATE");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("TB_ORDERS", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.OrderItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_ORDER_ITEM");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_ORDER");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_PRODUCT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("QUANTITY");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("TB_ORDER_ITEMS", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.PhoneEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_PHONE");

                    b.Property<string>("Ddd")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("DDD");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("NUMBER");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_SELLER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TYPE");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("TB_PHONES", (string)null);
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

                    b.ToTable("TB_PRODUCTS", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.SaleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_SALE");

                    b.Property<DateTime>("Moment")
                        .HasColumnType("TEXT")
                        .HasColumnName("MOMENT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_ORDER");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_SELLER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("TB_SALES", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.SellerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID_SELLER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("TEXT")
                        .HasColumnName("DS_EMAIL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("TEXT")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.ToTable("TB_SELLERS", (string)null);
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.OrderItemEntity", b =>
                {
                    b.HasOne("Payment_api.Domain.Entities.OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payment_api.Domain.Entities.ProductEntity", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.PhoneEntity", b =>
                {
                    b.HasOne("Payment_api.Domain.Entities.SellerEntity", null)
                        .WithMany("Phones")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payment_api.Domain.Entities.SellerEntity", "SellerEntity")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SellerEntity");
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

            modelBuilder.Entity("Payment_api.Domain.Entities.SaleEntity", b =>
                {
                    b.HasOne("Payment_api.Domain.Entities.OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payment_api.Domain.Entities.SellerEntity", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.ProductEntity", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Payment_api.Domain.Entities.SellerEntity", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
