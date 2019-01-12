using System;
using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models.Helpers
{
    public class TimeRange
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}