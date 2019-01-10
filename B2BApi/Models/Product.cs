using System;
using System.Collections;
using System.Collections.Generic;
using B2BApi.Models.Enum;

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
        
        public Dictionary<PriceType, string> Price { get; set; }
        public Dictionary<Competitor, string> CompetitorsPrices { get; set; }
        
        public string ProducerUri { get; set; }
        public Dictionary<Competitor, string> CompetitorsUri { get; set; }
        
        public DateTime UpdateTime { get; set; }
        
        public Dictionary<ProductAttribute, string> Attribute { get; set; }
    }
}