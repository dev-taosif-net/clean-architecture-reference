using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);

    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);

    Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
}