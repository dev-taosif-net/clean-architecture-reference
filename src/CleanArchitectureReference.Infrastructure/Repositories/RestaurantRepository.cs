using CleanArchitectureReference.Domain.Repositories;
using CleanArchitectureReference.Infrastructure.Persistence;

namespace CleanArchitectureReference.Infrastructure.Repositories;

public class RestaurantRepository(
    ApplicationDbContext dbContext,
    ILogger<RestaurantRepository> logger) : IRestaurantRepository
{
    public async Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Retrieving all restaurants");

        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.Count);

        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Retrieving restaurant {RestaurantId}", id);

        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (restaurant is null)
        {
            logger.LogWarning("Restaurant {RestaurantId} was not found", id);
        }

        return restaurant;
    }

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        await dbContext.Restaurants.AddAsync(restaurant, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Added restaurant {RestaurantId}", restaurant.Id);
    }

    public async Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        dbContext.Restaurants.Update(restaurant);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated restaurant {RestaurantId}", restaurant.Id);
    }

    public async Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted restaurant {RestaurantId}", restaurant.Id);
    }
}