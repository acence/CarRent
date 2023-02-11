using CarRent.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Database.Mappings
{
    public class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Make)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Model)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.UniqueId)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasMany(x => x.Rentals)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId);
        }
    }
}
