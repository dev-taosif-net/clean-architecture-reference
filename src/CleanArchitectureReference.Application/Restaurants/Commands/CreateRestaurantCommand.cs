using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.ValueObjects;

namespace CleanArchitectureReference.Application.Restaurants.Commands;

public record CreateRestaurantCommand(
    string Name,
    string Description,
    string Category,
    bool HasDelivery,
    string? ContactEmail,
    string? ContactNumber,
    string? City,
    string? Street,
    string? PostalCode) : ICommand<Guid>;

public class CreateRestaurantCommandHandler(IRestaurantRepository repository)
    : ICommandHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = new Restaurant
        {
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            HasDelivery = request.HasDelivery,
            ContactEmail = request.ContactEmail,
            ContactNumber = request.ContactNumber,
            Address = CreateAddress(request),
        };

        await repository.AddAsync(restaurant, cancellationToken);

        return restaurant.Id;
    }

    private static Address? CreateAddress(CreateRestaurantCommand request)
        => request is { City: null, Street: null, PostalCode: null }
            ? null
            : new Address(
                request.City ?? string.Empty,
                request.Street ?? string.Empty,
                request.PostalCode ?? string.Empty);
}
