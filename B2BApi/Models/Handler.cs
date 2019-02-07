using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace B2BApi.Models
{
    public class Handler
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public HandlerStatus Status { get; set; }
        public string Url { get; set; }
        public string SaveFileName { get; set; }
        public int StartRowData { get; set; }
       
        public ICollection<Pattern> Patterns { get; set; }
        public ICollection<GrabColumnItem> GrabColumnItems { get; set; }
        public ICollection<HandlerSettings> Settings { get; set; }
        
        public HandlerScheduler HandlerScheduler { get; set; }
        public DateTime LastUpdate { get; set; }

        #region Navigation properties
        public Provider Provider { get; set; }

        #endregion
    }
}