using System;
using System.Collections;
using System.Collections.Generic;
using B2BApi.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B2BApi.Models
{   
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public Brand Brand { get; set; }
        public BrandType BrandType { get; set; }
        public string PartNumber { get; set; }
        public string Gtin { get; set; }
        public Category Category { get; set; }
        
        public ICollection<StockProduct> Stocks { get; set; }
        
        public ICollection<Price> Price { get; set; }
        public ICollection<CompetitorsPrices> CompetitorsPrices { get; set; }
        
        public string ProducerUri { get; set; }
        public ICollection<CompetitorsUri> CompetitorsUri { get; set; }
        
        public DateTime UpdateTime { get; set; }
        
        public ICollection<Attribute> Attribute { get; set; }
    }
    
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Model);
            builder.Property(x => x.PartNumber);
            builder.Property(x => x.Gtin);
            builder.Property(x => x.ProducerUri);
            builder.Property(x => x.Price);
            builder.Property(x => x.CompetitorsPrices);
            builder.Property(x => x.CompetitorsUri);
            builder.Property(x => x.Attribute);
            builder.Property(x => x.UpdateTime)
                .HasConversion(new DateTimeToStringConverter());
            
            builder.HasOne<Brand>(x => x.Brand);
            builder.HasOne<BrandType>(x => x.BrandType);
            builder.HasOne<Category>(x => x.Category);
            
            builder.HasMany<StockProduct>(x => x.Stocks);


        }
    }
}