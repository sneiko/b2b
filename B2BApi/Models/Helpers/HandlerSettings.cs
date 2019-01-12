using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models.Helpers
{
    public class HandlerSettings
    {
        [Key]
        public int Id { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}