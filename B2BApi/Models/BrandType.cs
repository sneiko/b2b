using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models
{
    public class BrandType
    {
        [Key]
        public int Id { get; set; }
        public string Name  { get; set; }
        public Brand Brand { get; set; }
    }
}