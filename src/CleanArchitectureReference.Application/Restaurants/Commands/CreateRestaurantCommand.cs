using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.ValueObjects;
using FluentValidation;

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

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Category)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .MaximumLength(256)
            .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));

        RuleFor(x => x.ContactNumber)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.ContactNumber));

        RuleFor(x => x.City)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.Street)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.PostalCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
    }
}
