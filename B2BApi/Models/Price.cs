using B2BApi.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B2BApi.Models
{
    public class Price
    {
        public PriceType PriceType { get; set; }
        public string Value { get; set; }
    }
    
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasOne(x => x.PriceType);
            builder.Property(x => x.Value);
        }
    }
}