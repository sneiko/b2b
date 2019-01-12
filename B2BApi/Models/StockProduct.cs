using System;
using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models
{
    public class StockProduct
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public Provider Provider { get; set; }
        public int Count { get; set; }
        public DateTime UpdateTime { get; set; }    
    }
}