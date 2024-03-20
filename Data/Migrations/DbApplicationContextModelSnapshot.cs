﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DbApplicationContext))]
    partial class DbApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Purchases.Purchase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CheckoutTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("PurchaserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<float>("TotalCost")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PurchaserId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Domain.Purchases.PurchasedItem", b =>
                {
                    b.Property<long>("PurchaseId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("PurchaseId", "ItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseId", "ItemId");

                    b.ToTable("PurchasedItems");
                });

            modelBuilder.Entity("Domain.StoreItems.StoreItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<long?>("SellerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("StoreItems");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Contacts")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Purchases.Purchase", b =>
                {
                    b.HasOne("Domain.Users.User", "Purchaser")
                        .WithMany("Purchases")
                        .HasForeignKey("PurchaserId");

                    b.Navigation("Purchaser");
                });

            modelBuilder.Entity("Domain.Purchases.PurchasedItem", b =>
                {
                    b.HasOne("Domain.StoreItems.StoreItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Purchases.Purchase", "Purchase")
                        .WithMany("Items")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Domain.StoreItems.StoreItem", b =>
                {
                    b.HasOne("Domain.Users.User", "Seller")
                        .WithMany("OfferedItems")
                        .HasForeignKey("SellerId");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Domain.Purchases.Purchase", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("OfferedItems");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
