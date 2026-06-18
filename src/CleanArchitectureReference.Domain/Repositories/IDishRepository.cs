using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Domain.Repositories;

public interface IDishRepository
{
    Task<IReadOnlyList<Dish>> GetAllByRestaurantIdAsync(
        Guid restaurantId, CancellationToken cancellationToken = default);

    Task<Dish?> GetByIdAsync(Guid restaurantId, int id, CancellationToken cancellationToken = default);

    Task AddAsync(Dish dish, CancellationToken cancellationToken = default);

    Task UpdateAsync(Dish dish, CancellationToken cancellationToken = default);

    Task DeleteAsync(Dish dish, CancellationToken cancellationToken = default);
}
