namespace CleanArchitectureReference.Application.Restaurants.Dtos;

public record DishDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int? KiloCalories);