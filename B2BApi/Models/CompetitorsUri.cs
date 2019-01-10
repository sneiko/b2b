using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2BApi.Models
{
    public class CompetitorsUri
    {
        public Competitor Competitor { get; set; }
        public string Value { get; set; }
    }
    
    public class CompetitorsUriConfiguration : IEntityTypeConfiguration<CompetitorsUri>
    {
        public void Configure(EntityTypeBuilder<CompetitorsUri> builder)
        {
            builder.HasOne(x => x.Competitor);
            builder.Property(x => x.Value);
        }
    }
}