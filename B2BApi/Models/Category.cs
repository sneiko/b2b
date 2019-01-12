using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Helpers;

namespace B2BApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public ShopCategoryId ShopCategoryId { get; set; }
        public ICollection<AttributeRow> Attribute { get; set; }
    }
}