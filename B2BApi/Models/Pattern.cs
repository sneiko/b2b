using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;

namespace B2BApi.Models
{
    public class Pattern
    {
        [Key]
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
    }
}