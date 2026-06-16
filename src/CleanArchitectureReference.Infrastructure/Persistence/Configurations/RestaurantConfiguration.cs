namespace CleanArchitectureReference.Infrastructure.Persistence.Configurations;

internal sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(r => r.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.ContactEmail)
            .HasMaxLength(256);

        builder.Property(r => r.ContactNumber)
            .HasMaxLength(20);

        builder.OwnsOne(r => r.Address, address =>
        {
            address.Property(a => a.City).HasMaxLength(100);
            address.Property(a => a.Street).HasMaxLength(100);
            address.Property(a => a.PostalCode).HasMaxLength(10);
        });

        builder.HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
