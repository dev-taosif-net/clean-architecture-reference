namespace CleanArchitectureReference.Application.Restaurants.Dtos;

public record RestaurantDto(
    Guid Id,
    string Name,
    string Description,
    string Category,
    bool HasDelivery,
    string? ContactEmail,
    string? ContactNumber,
    string? City,
    string? Street,
    string? PostalCode,
    IReadOnlyList<DishDto> Dishes);