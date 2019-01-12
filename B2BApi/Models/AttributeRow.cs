using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models
{
    public class AttributeRow
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}