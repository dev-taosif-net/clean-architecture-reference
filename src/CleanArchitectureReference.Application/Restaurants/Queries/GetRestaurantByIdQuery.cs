using CleanArchitectureReference.Application.Restaurants.Mappings;

namespace CleanArchitectureReference.Application.Restaurants.Queries;

public record GetRestaurantByIdQuery(Guid Id) : IRequest<RestaurantDto?>;

public class GetRestaurantByIdQueryHandler(IRestaurantRepository repository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(
        GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id, cancellationToken);

        return restaurant?.ToDto();
    }
}
