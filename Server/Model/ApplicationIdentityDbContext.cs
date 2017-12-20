using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Model {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)
            : base(optionsBuilder){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        }

        //table
        public DbSet<Chemistry> Chemistries {get;set;}
        public DbSet<Employee> Employees {get;set;}
        public DbSet<Famer> Famers {get;set;}
        public DbSet<Fertilizer> Fertilizers {get;set;}
        public DbSet<FertilizerType> FertilizerTypes {get;set;}
        public DbSet<Harvest> Harvest {get;set;}
        public DbSet<LandArea> LandAreas {get;set;}
        public DbSet<LandUsage> LandUsage {get;set;}
        public DbSet<Personal> Personal {get;set;}
        public DbSet<ProfileData> ProfileDatas {get;set;}
        public DbSet<ResourcesExport> ResourcesExport {get;set;}
        public DbSet<ResourcesUsage> ResourcesUsage {get;set;}
        public DbSet<Seed> Seeds {get;set;}
        public DbSet<SeedProfile> SeedProfile {get;set;}
        
    }
}