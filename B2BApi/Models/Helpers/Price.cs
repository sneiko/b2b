using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;

namespace B2BApi.Models.Helpers
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public PriceType PriceType { get; set; }
        public string Value { get; set; }
    }
    
//    public class PriceConfiguration : IEntityTypeConfiguration<Price>
//    {
//        public void Configure(EntityTypeBuilder<Price> builder)
//        {
//            builder.HasOne(x => x.PriceType);
//            builder.Property(x => x.Value);
//        }
//    }
}