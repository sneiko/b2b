using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using B2BApi.Interfaces;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using Newtonsoft.Json;

namespace B2BApi.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public PriceType PriceType { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime UpdateTime { get; set; }

        #region Navigation Properties

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product Product { get; set; }

        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        [JsonIgnore]
        public Provider Provider { get; set; }

        #endregion
    }
}