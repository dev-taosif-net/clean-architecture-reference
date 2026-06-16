using CleanArchitectureReference.Application.Restaurants.Dtos;
using CleanArchitectureReference.Application.Restaurants.Mappings;
using CleanArchitectureReference.Domain.Repositories;
using MediatR;

namespace CleanArchitectureReference.Application.Restaurants.Queries.GetRestaurantById;

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