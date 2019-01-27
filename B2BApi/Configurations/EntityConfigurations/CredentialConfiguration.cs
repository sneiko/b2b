using B2BApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2BApi.EntityConfigurations
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credentials>
    {
        public void Configure(EntityTypeBuilder<Credentials> builder)
        {
            builder.Property<int>("UserId");
            
            builder.HasKey("UserId");

            builder.Property(x => x.Password)
                .IsRequired();
            
            builder.HasOne(x => x.User)
                .WithOne(x => x.Credentials)
                .HasForeignKey<Credentials>("UserId");
            
        }
    }
}