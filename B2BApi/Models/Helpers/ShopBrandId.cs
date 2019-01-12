using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models.Helpers
{
    public class ShopBrandId
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
    }
}