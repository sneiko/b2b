using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models
{
    // Конкурент
    public class Competitor
    {
        [Key]
        public int Id { get; set; }
    }
}