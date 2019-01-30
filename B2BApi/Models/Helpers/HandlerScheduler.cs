using System;
using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models.Helpers
{
    public class HandlerScheduler
    {
        [Key]
        public int Id { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}