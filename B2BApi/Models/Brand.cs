using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Helpers;

namespace B2BApi.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandType { get; set; }
        public ICollection<ShopBrandId> ShopBrandId { get; set; }
    }
}