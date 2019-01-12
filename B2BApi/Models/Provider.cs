using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;

namespace B2BApi.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        public ProviderType ProviderType { get; set; }
        public string Name { get; set; }
        public string Bank { get; set; }
        public string KorSchet { get; set; }
        public string RasSchet { get; set; }
        public string Bic { get; set; }
        public string Inn { get; set; }
        public string uAddress { get; set; }
        public ICollection<ProviderContact> Contacts { get; set; }
        public TimeRange OfficeWorkTimeRange { get; set; }
        public TimeRange StockWorkTimeRange { get; set; }
        public string StockAddress { get; set; }
        public DateTime RequestDeadline { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public string Currency { get; set; }
    }
}