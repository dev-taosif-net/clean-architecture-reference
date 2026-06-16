namespace CleanArchitectureReference.Infrastructure.Seeders;

public interface IRestaurantSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}