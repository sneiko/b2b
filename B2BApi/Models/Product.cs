using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Helpers;

namespace B2BApi.Models
{   
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public Brand Brand { get; set; }
        public BrandType BrandType { get; set; }
        public string PartNumber { get; set; }
        public string Gtin { get; set; }
        public Category Category { get; set; } 
        public string ProducerUri { get; set; } 
        public DateTime UpdateTime { get; set; }
        
        public ICollection<StockProduct> Stocks { get; set; }
        public ICollection<Price> Price { get; set; }
        public ICollection<CompetitorsPrices> CompetitorsPrices { get; set; } 
        public ICollection<CompetitorsUri> CompetitorsUri { get; set; } 
        public ICollection<AttributeRow> Attribute { get; set; }
    }
}
