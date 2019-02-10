using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using B2BApi.Models.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace B2BApi.Models
{   
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public string PartNumber { get; set; }
        public string Gtin { get; set; }
        public Category Category { get; set; } 
        public string ProducerUri { get; set; } 
        public DateTime UpdateTime { get; set; }
        
        #region Navigation properties
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public List<Stock> Stocks { get; set; }
        #endregion
        
        public ICollection<CompetitorsPrices> CompetitorsPrices { get; set; } 
        public ICollection<CompetitorsUri> CompetitorsUri { get; set; } 
        public ICollection<AttributeRow> Attribute { get; set; }
    }
}
