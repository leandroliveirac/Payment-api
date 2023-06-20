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
    [Migration("20221117161054_Add descrption field from Categories table for alternate key")]
    partial class AdddescrptionfieldfromCategoriestableforalternatekey
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

                    b.HasAlternateKey("Description");

                    b.ToTable("CATEGORIES", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
