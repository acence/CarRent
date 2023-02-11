using CarRent.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Database.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasMany(x => x.Rentals)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
