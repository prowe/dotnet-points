﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("AccountEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PointChange")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountEvents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BalanceAdjustmentEvent", b =>
                {
                    b.HasBaseType("AccountEvent");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("BalanceAdjustmentEvent");
                });

            modelBuilder.Entity("DepositPointsEvent", b =>
                {
                    b.HasBaseType("AccountEvent");

                    b.HasDiscriminator().HasValue("DepositPointsEvent");
                });

            modelBuilder.Entity("RedeemPointsEvent", b =>
                {
                    b.HasBaseType("AccountEvent");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("RedeemPointsEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
