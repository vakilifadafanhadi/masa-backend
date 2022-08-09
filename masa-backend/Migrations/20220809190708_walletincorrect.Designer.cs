﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using masa_backend;

#nullable disable

namespace masa_backend.Migrations
{
    [DbContext(typeof(MasaDbContext))]
    [Migration("20220809190708_walletincorrect")]
    partial class walletincorrect
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("masa_backend.Models.City", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CityCode")
                        .HasColumnType("int");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProvinceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("masa_backend.Models.Country", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Continent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("masa_backend.Models.PersonalInformation", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("ProvinceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("NationalCode")
                        .IsUnique()
                        .HasFilter("[NationalCode] IS NOT NULL");

                    b.HasIndex("ProvinceId");

                    b.ToTable("PersonalInformations");
                });

            modelBuilder.Entity("masa_backend.Models.Province", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinceCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("masa_backend.Models.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique()
                        .HasFilter("[PersonId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("masa_backend.Models.Wallet", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("WalletHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique()
                        .HasFilter("[PersonId] IS NOT NULL");

                    b.HasIndex("WalletHistoryId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("masa_backend.Models.WalletHistory", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DtoRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Transaction")
                        .HasColumnType("bit");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("WalletHistories");
                });

            modelBuilder.Entity("masa_backend.Models.City", b =>
                {
                    b.HasOne("masa_backend.Models.Country", "Country")
                        .WithMany("Citys")
                        .HasForeignKey("CountryId");

                    b.HasOne("masa_backend.Models.Province", "Province")
                        .WithMany("Citys")
                        .HasForeignKey("ProvinceId");

                    b.Navigation("Country");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("masa_backend.Models.PersonalInformation", b =>
                {
                    b.HasOne("masa_backend.Models.City", "City")
                        .WithMany("PersonalInformation")
                        .HasForeignKey("CityId");

                    b.HasOne("masa_backend.Models.Country", "Country")
                        .WithMany("PersonalInformation")
                        .HasForeignKey("CountryId");

                    b.HasOne("masa_backend.Models.Province", "Province")
                        .WithMany("PersonalInformation")
                        .HasForeignKey("ProvinceId");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("masa_backend.Models.Province", b =>
                {
                    b.HasOne("masa_backend.Models.Country", "Country")
                        .WithMany("Provinces")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("masa_backend.Models.User", b =>
                {
                    b.HasOne("masa_backend.Models.PersonalInformation", "PersonalInformation")
                        .WithOne("User")
                        .HasForeignKey("masa_backend.Models.User", "PersonId");

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("masa_backend.Models.Wallet", b =>
                {
                    b.HasOne("masa_backend.Models.PersonalInformation", "PersonalInformation")
                        .WithOne("Wallet")
                        .HasForeignKey("masa_backend.Models.Wallet", "PersonId");

                    b.HasOne("masa_backend.Models.WalletHistory", "WalletHistory")
                        .WithMany("Wallets")
                        .HasForeignKey("WalletHistoryId");

                    b.Navigation("PersonalInformation");

                    b.Navigation("WalletHistory");
                });

            modelBuilder.Entity("masa_backend.Models.City", b =>
                {
                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("masa_backend.Models.Country", b =>
                {
                    b.Navigation("Citys");

                    b.Navigation("PersonalInformation");

                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("masa_backend.Models.PersonalInformation", b =>
                {
                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("masa_backend.Models.Province", b =>
                {
                    b.Navigation("Citys");

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("masa_backend.Models.WalletHistory", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
