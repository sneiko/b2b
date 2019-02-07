using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using B2BApi.Interfaces;
using B2BApi.Models.Helpers;
using Newtonsoft.Json;

namespace B2BApi.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public Price Price { get; set; }
        public int Count { get; set; }
        public DateTime UpdateTime { get; set; }

        #region Navigation Properties

        [JsonIgnore]
        public Product Product { get; set; }
        [JsonIgnore]
        public Provider Provider { get; set; }

        #endregion
    }
}