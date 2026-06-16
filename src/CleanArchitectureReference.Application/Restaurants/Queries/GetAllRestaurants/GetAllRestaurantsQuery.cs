using CleanArchitectureReference.Application.Restaurants.Dtos;
using MediatR;

namespace CleanArchitectureReference.Application.Restaurants.Queries.GetAllRestaurants;

public record GetAllRestaurantsQuery : IRequest<IReadOnlyList<RestaurantDto>>;