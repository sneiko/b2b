using System;
using System.Collections.Generic;
using B2BApi.EntityConfigurations;
using B2BApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Attribute = System.Attribute;

namespace B2BApi.DbContext
{
    public class B2BDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public B2BDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=b2b.db");
        }

        // general
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandType> BrandTypes { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<AttributeRow> Attributes { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
        public DbSet<Handler> Handlers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<User> Users { get; set; }
       
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Handler
            modelBuilder.Entity<Handler>()
                .HasOne(p => p.Provider)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CredentialConfiguration());
        }
        
    }
}