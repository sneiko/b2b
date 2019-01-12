﻿// <auto-generated />
using System;
using B2BApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B2BApi.Migrations
{
    [DbContext(typeof(B2BDbContext))]
    [Migration("20190112001412_0001_CreateModels")]
    partial class _0001_CreateModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("B2BApi.Models.AttributeRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("ProductId");

                    b.Property<string>("Type");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("B2BApi.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("B2BApi.Models.BrandType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrandId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("BrandTypes");
                });

            modelBuilder.Entity("B2BApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Parent");

                    b.Property<int?>("ShopCategoryIdId");

                    b.HasKey("Id");

                    b.HasIndex("ShopCategoryIdId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("B2BApi.Models.Competitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Competitors");
                });

            modelBuilder.Entity("B2BApi.Models.Handler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name");

                    b.Property<int?>("ProviderId");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Handlers");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.CompetitorsPrices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompetitorId");

                    b.Property<int?>("ProductId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CompetitorId");

                    b.HasIndex("ProductId");

                    b.ToTable("CompetitorsPrices");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.CompetitorsUri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompetitorId");

                    b.Property<int?>("ProductId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CompetitorId");

                    b.HasIndex("ProductId");

                    b.ToTable("CompetitorsUri");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.HandlerSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Field");

                    b.Property<int?>("HandlerId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.ToTable("HandlerSettings");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PriceType");

                    b.Property<int?>("ProductId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.PriceColumnItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HandlerId");

                    b.Property<int>("PriceColumn");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.ToTable("PriceColumnItem");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.ProviderContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int?>("ProviderId");

                    b.Property<int>("ProviderType");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("ProviderContact");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.ShopBrandId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrandId");

                    b.Property<string>("TypeName");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("ShopBrandId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.ShopCategoryId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ShopCategoryId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.TimeRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.ToTable("TimeRange");
                });

            modelBuilder.Entity("B2BApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrandId");

                    b.Property<int?>("BrandTypeId");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Gtin");

                    b.Property<string>("Model");

                    b.Property<string>("PartNumber");

                    b.Property<string>("ProducerUri");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("BrandTypeId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("B2BApi.Models.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bank");

                    b.Property<string>("Bic");

                    b.Property<string>("Currency");

                    b.Property<string>("Inn");

                    b.Property<string>("KorSchet");

                    b.Property<string>("Name");

                    b.Property<int?>("OfficeWorkTimeRangeId");

                    b.Property<int>("ProviderType");

                    b.Property<string>("RasSchet");

                    b.Property<DateTime>("RequestDeadline");

                    b.Property<string>("StockAddress");

                    b.Property<int?>("StockWorkTimeRangeId");

                    b.Property<DateTime>("TimeOfDelivery");

                    b.Property<string>("uAddress");

                    b.HasKey("Id");

                    b.HasIndex("OfficeWorkTimeRangeId");

                    b.HasIndex("StockWorkTimeRangeId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("B2BApi.Models.StockProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("StockProducts");
                });

            modelBuilder.Entity("B2BApi.Models.AttributeRow", b =>
                {
                    b.HasOne("B2BApi.Models.Category")
                        .WithMany("Attribute")
                        .HasForeignKey("CategoryId");

                    b.HasOne("B2BApi.Models.Product")
                        .WithMany("Attribute")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("B2BApi.Models.BrandType", b =>
                {
                    b.HasOne("B2BApi.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");
                });

            modelBuilder.Entity("B2BApi.Models.Category", b =>
                {
                    b.HasOne("B2BApi.Models.Helpers.ShopCategoryId", "ShopCategoryId")
                        .WithMany()
                        .HasForeignKey("ShopCategoryIdId");
                });

            modelBuilder.Entity("B2BApi.Models.Handler", b =>
                {
                    b.HasOne("B2BApi.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.CompetitorsPrices", b =>
                {
                    b.HasOne("B2BApi.Models.Competitor", "Competitor")
                        .WithMany()
                        .HasForeignKey("CompetitorId");

                    b.HasOne("B2BApi.Models.Product")
                        .WithMany("CompetitorsPrices")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.CompetitorsUri", b =>
                {
                    b.HasOne("B2BApi.Models.Competitor", "Competitor")
                        .WithMany()
                        .HasForeignKey("CompetitorId");

                    b.HasOne("B2BApi.Models.Product")
                        .WithMany("CompetitorsUri")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.HandlerSettings", b =>
                {
                    b.HasOne("B2BApi.Models.Handler")
                        .WithMany("Settings")
                        .HasForeignKey("HandlerId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.Price", b =>
                {
                    b.HasOne("B2BApi.Models.Product")
                        .WithMany("Price")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.PriceColumnItem", b =>
                {
                    b.HasOne("B2BApi.Models.Handler")
                        .WithMany("PriceColumnItems")
                        .HasForeignKey("HandlerId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.ProviderContact", b =>
                {
                    b.HasOne("B2BApi.Models.Provider")
                        .WithMany("Contacts")
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("B2BApi.Models.Helpers.ShopBrandId", b =>
                {
                    b.HasOne("B2BApi.Models.Brand")
                        .WithMany("ShopBrandId")
                        .HasForeignKey("BrandId");
                });

            modelBuilder.Entity("B2BApi.Models.Product", b =>
                {
                    b.HasOne("B2BApi.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("B2BApi.Models.BrandType", "BrandType")
                        .WithMany()
                        .HasForeignKey("BrandTypeId");

                    b.HasOne("B2BApi.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("B2BApi.Models.Provider", b =>
                {
                    b.HasOne("B2BApi.Models.Helpers.TimeRange", "OfficeWorkTimeRange")
                        .WithMany()
                        .HasForeignKey("OfficeWorkTimeRangeId");

                    b.HasOne("B2BApi.Models.Helpers.TimeRange", "StockWorkTimeRange")
                        .WithMany()
                        .HasForeignKey("StockWorkTimeRangeId");
                });

            modelBuilder.Entity("B2BApi.Models.StockProduct", b =>
                {
                    b.HasOne("B2BApi.Models.Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
