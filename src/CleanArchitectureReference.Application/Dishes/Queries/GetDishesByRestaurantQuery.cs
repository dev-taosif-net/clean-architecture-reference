using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;

namespace CleanArchitectureReference.Application.Dishes.Queries;

public record GetDishesByRestaurantQuery(Guid RestaurantId) : IQuery<IReadOnlyList<DishDto>>;

public class GetDishesByRestaurantQueryHandler(
    IDishRepository dishRepository,
    IRestaurantRepository restaurantRepository)
    : IQueryHandler<GetDishesByRestaurantQuery, IReadOnlyList<DishDto>>
{
    public async Task<IReadOnlyList<DishDto>> Handle(
        GetDishesByRestaurantQuery request, CancellationToken cancellationToken)
    {
        _ = await restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId);

        var dishes = await dishRepository.GetAllByRestaurantIdAsync(request.RestaurantId, cancellationToken);

        return dishes.Adapt<IReadOnlyList<DishDto>>();
    }
}
