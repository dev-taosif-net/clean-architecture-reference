using CleanArchitectureReference.Application.Restaurants.Dtos;
using CleanArchitectureReference.Application.Restaurants.Mappings;
using CleanArchitectureReference.Domain.Repositories;
using MediatR;

namespace CleanArchitectureReference.Application.Restaurants.Queries.GetAllRestaurants;

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