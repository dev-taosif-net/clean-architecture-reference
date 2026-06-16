using CleanArchitectureReference.Application.Restaurants.Dtos;
using MediatR;

namespace CleanArchitectureReference.Application.Restaurants.Queries.GetRestaurantById;

public record GetRestaurantByIdQuery(Guid Id) : IRequest<RestaurantDto?>;