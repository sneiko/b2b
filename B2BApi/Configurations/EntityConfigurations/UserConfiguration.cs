using B2BApi.Models;
using B2BApi.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2BApi.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
                .HasDiscriminator<UserRole>("Role")
                .HasValue<Admin>(UserRole.Admin)
                .HasValue<Manager>(UserRole.Manager)
                .HasValue<Director>(UserRole.Director);
            
            builder.Property<UserRole>("Role").Metadata.AfterSaveBehavior = PropertySaveBehavior.Save;

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.UserName)
                .HasMaxLength(100);
            
            builder.HasIndex(x => x.UserName)
                .IsUnique();
            
            builder.Property(x => x.Status)
                .HasDefaultValue(UserStatus.Active);

            builder.HasOne(x => x.Token)
                .WithOne(x => x.User)
                .HasForeignKey<CompleteToken>(x=>x.UserId);
        }
    }
}