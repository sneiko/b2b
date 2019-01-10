using System;
using System.Collections.Generic;
using B2BApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace B2BApi.DbContext
{
    public class B2BDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=b2b.db");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandType> BrandTypes { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
       
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PriceConfiguration());
            modelBuilder.ApplyConfiguration(new CompetitorsPricesConfiguration());
            modelBuilder.ApplyConfiguration(new CompetitorsUriConfiguration());
        }
        
    }
}