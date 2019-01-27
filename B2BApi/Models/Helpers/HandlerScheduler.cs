using System;
using System.ComponentModel.DataAnnotations;

namespace B2BApi.Models.Helpers
{
    public class HandlerScheduler
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan StartOnTime { get; set; }
        public int StartEveryDay { get; set; }
    }
}