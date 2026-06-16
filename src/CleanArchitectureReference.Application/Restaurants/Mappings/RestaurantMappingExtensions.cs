using CleanArchitectureReference.Application.Restaurants.Dtos;
using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Application.Restaurants.Mappings;

public static class RestaurantMappingExtensions
{
    public static RestaurantDto ToDto(this Restaurant restaurant)
        => new(
            restaurant.Id,
            restaurant.Name,
            restaurant.Description,
            restaurant.Category,
            restaurant.HasDelivery,
            restaurant.ContactEmail,
            restaurant.ContactNumber,
            restaurant.Address?.City,
            restaurant.Address?.Street,
            restaurant.Address?.PostalCode,
            restaurant.Dishes.Select(d => d.ToDto()).ToList());

    public static DishDto ToDto(this Dish dish)
        => new(dish.Id, dish.Name, dish.Description, dish.Price, dish.KiloCalories);
}