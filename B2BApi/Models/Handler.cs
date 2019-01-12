using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace B2BApi.Models
{
    public class Handler
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Provider Provider { get; set; }
       
        public ICollection<PriceColumnItem> PriceColumnItems { get; set; }
        public ICollection<HandlerSettings> Settings { get; set; }
        
        public DateTime LastUpdate { get; set; }
    }
}