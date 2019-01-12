using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2BApi.Models.Helpers
{
    public class CompetitorsUri
    {
        [Key]
        public int Id { get; set; }
        public Competitor Competitor { get; set; }
        public string Value { get; set; }
    }
}