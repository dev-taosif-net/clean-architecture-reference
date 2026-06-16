using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}