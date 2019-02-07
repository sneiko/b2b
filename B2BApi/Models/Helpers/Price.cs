using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;

namespace B2BApi.Models.Helpers
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public PriceType PriceType { get; set; }
        public double Value { get; set; }
    }
}