using CleanArchitectureReference.Domain.Repositories;
using CleanArchitectureReference.Infrastructure.Persistence;

namespace CleanArchitectureReference.Infrastructure.Repositories;

public class RestaurantRepository(ApplicationDbContext dbContext) : IRestaurantRepository
{
    public async Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbContext.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        await dbContext.Restaurants.AddAsync(restaurant, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}