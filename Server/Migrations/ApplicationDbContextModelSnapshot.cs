﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QuanLyNongTrai.Model;
using System;

namespace Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<bool>("PasswordChanged");

                    b.Property<string>("PasswordHash");

                    b.Property<Guid>("PersonalId");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Chemistry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Chemistries");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime?>("EndWorkTime");

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("PersonalId");

                    b.Property<decimal>("Salary");

                    b.Property<DateTime>("StartWorkTime");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Famer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("PersonalId");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.ToTable("Famers");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Fertilizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<Guid>("FertilizerTypeId");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FertilizerTypeId");

                    b.ToTable("Fertilizers");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.FertilizerType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("FertilizerTypes");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Harvest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime>("GatherTime");

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("LandUsageId");

                    b.Property<float>("Quantity");

                    b.Property<Guid>("SeedId");

                    b.HasKey("Id");

                    b.HasIndex("LandUsageId");

                    b.HasIndex("SeedId");

                    b.ToTable("Harvest");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.LandArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LandCode")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("Latitude")
                        .HasMaxLength(20);

                    b.Property<string>("Longitude")
                        .HasMaxLength(20);

                    b.Property<bool>("OfCompany");

                    b.HasKey("Id");

                    b.ToTable("LandAreas");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.LandUsage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("FamerId");

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("LandAreaId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("FamerId");

                    b.HasIndex("LandAreaId")
                        .IsUnique();

                    b.ToTable("LandUsage");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Personal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("BirthDay");

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<bool>("Sex");

                    b.HasKey("Id");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ProfileData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("ProfileDatas");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ResourcesExport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime>("ExportTime");

                    b.Property<Guid>("FamerId");

                    b.Property<bool>("IsDelete");

                    b.Property<float>("Quantity");

                    b.Property<Guid>("ResourcesId");

                    b.Property<string>("ResourcesType")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.HasIndex("FamerId");

                    b.ToTable("ResourcesExport");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ResourcesUsage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("LandUsageId");

                    b.Property<float>("Quantity");

                    b.Property<Guid>("ResourcesId");

                    b.Property<string>("ResourcesType")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<DateTime>("UsageTime");

                    b.HasKey("Id");

                    b.HasIndex("LandUsageId");

                    b.ToTable("ResourcesUsage");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Seed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Seeds");
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.SeedProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("ProfileDataId");

                    b.Property<Guid>("SeedId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileDataId");

                    b.HasIndex("SeedId");

                    b.ToTable("SeedProfile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ApplicationUser", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.Personal", "Personal")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("QuanLyNongTrai.Model.Entity.ApplicationUser", "PersonalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Employee", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.Personal", "Personal")
                        .WithOne("Employee")
                        .HasForeignKey("QuanLyNongTrai.Model.Entity.Employee", "PersonalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Famer", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.Personal", "Personal")
                        .WithOne("Famer")
                        .HasForeignKey("QuanLyNongTrai.Model.Entity.Famer", "PersonalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Fertilizer", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.FertilizerType", "FertilizerType")
                        .WithMany("Fertilizers")
                        .HasForeignKey("FertilizerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.Harvest", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.LandUsage", "LandUsage")
                        .WithMany("Harvests")
                        .HasForeignKey("LandUsageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuanLyNongTrai.Model.Entity.Seed", "Seed")
                        .WithMany("Harvests")
                        .HasForeignKey("SeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.LandUsage", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.Famer", "Famer")
                        .WithMany("LandUsages")
                        .HasForeignKey("FamerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuanLyNongTrai.Model.Entity.LandArea", "LandArea")
                        .WithOne("LandUsage")
                        .HasForeignKey("QuanLyNongTrai.Model.Entity.LandUsage", "LandAreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ResourcesExport", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.Famer", "Famer")
                        .WithMany("ResourcesExports")
                        .HasForeignKey("FamerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.ResourcesUsage", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.LandUsage", "LandUsage")
                        .WithMany("ResourcesUsages")
                        .HasForeignKey("LandUsageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuanLyNongTrai.Model.Entity.SeedProfile", b =>
                {
                    b.HasOne("QuanLyNongTrai.Model.Entity.ProfileData", "ProfileData")
                        .WithMany("SeedProfiles")
                        .HasForeignKey("ProfileDataId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuanLyNongTrai.Model.Entity.Seed", "Seed")
                        .WithMany("SeedProfiles")
                        .HasForeignKey("SeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
