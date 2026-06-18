using CleanArchitectureReference.Domain.Repositories;
using CleanArchitectureReference.Infrastructure.Persistence;

namespace CleanArchitectureReference.Infrastructure.Repositories;

public class DishRepository(
    ApplicationDbContext dbContext,
    ILogger<DishRepository> logger) : IDishRepository
{
    public async Task<IReadOnlyList<Dish>> GetAllByRestaurantIdAsync(
        Guid restaurantId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Retrieving all dishes for restaurant {RestaurantId}", restaurantId);

        var dishes = await dbContext.Dishes
            .Where(d => d.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "Retrieved {DishCount} dishes for restaurant {RestaurantId}", dishes.Count, restaurantId);

        return dishes;
    }

    public async Task<Dish?> GetByIdAsync(
        Guid restaurantId, int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Retrieving dish {DishId} for restaurant {RestaurantId}", id, restaurantId);

        var dish = await dbContext.Dishes
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id, cancellationToken);

        if (dish is null)
        {
            logger.LogWarning(
                "Dish {DishId} for restaurant {RestaurantId} was not found", id, restaurantId);
        }

        return dish;
    }

    public async Task AddAsync(Dish dish, CancellationToken cancellationToken = default)
    {
        await dbContext.Dishes.AddAsync(dish, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Added dish {DishId}", dish.Id);
    }

    public async Task UpdateAsync(Dish dish, CancellationToken cancellationToken = default)
    {
        dbContext.Dishes.Update(dish);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated dish {DishId}", dish.Id);
    }

    public async Task DeleteAsync(Dish dish, CancellationToken cancellationToken = default)
    {
        dbContext.Dishes.Remove(dish);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted dish {DishId}", dish.Id);
    }
}
