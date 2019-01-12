using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;

namespace B2BApi.Models.Helpers
{
    public class PriceColumnItem
    {
        [Key]
        public int Id { get; set; }
        public PriceColumn PriceColumn { get; set; }
        public string Value { get; set; }
    }
}