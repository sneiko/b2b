using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2BApi.Models
{
    public class CompetitorsPrices
    {
        public Competitor Competitor { get; set; }
        public string Value { get; set; }
    }
    
    public class CompetitorsPricesConfiguration : IEntityTypeConfiguration<CompetitorsPrices>
    {
        public void Configure(EntityTypeBuilder<CompetitorsPrices> builder)
        {
            builder.HasOne(x => x.Competitor);
            builder.Property(x => x.Value);
        }
    }
}