using CleanArchitectureReference.Application.Restaurants.Mappings;

namespace CleanArchitectureReference.Application.Restaurants.Queries;

public record GetAllRestaurantsQuery : IRequest<IReadOnlyList<RestaurantDto>>;

public class GetAllRestaurantsQueryHandler(IRestaurantRepository repository)
    : IRequestHandler<GetAllRestaurantsQuery, IReadOnlyList<RestaurantDto>>
{
    public async Task<IReadOnlyList<RestaurantDto>> Handle(
        GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await repository.GetAllAsync(cancellationToken);

        return restaurants.Select(r => r.ToDto()).ToList();
    }
}