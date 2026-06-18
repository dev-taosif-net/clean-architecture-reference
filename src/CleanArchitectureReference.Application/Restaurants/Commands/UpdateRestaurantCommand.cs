using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;
using CleanArchitectureReference.Domain.ValueObjects;
using FluentValidation;

namespace CleanArchitectureReference.Application.Restaurants.Commands;

public record UpdateRestaurantCommand(
    Guid Id,
    string Name,
    string Description,
    string Category,
    bool HasDelivery,
    string? ContactEmail,
    string? ContactNumber,
    string? City,
    string? Street,
    string? PostalCode) : ICommand;

public class UpdateRestaurantCommandHandler(IRestaurantRepository repository)
    : ICommandHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id);

        restaurant.Name = request.Name;
        restaurant.Description = request.Description;
        restaurant.Category = request.Category;
        restaurant.HasDelivery = request.HasDelivery;
        restaurant.ContactEmail = request.ContactEmail;
        restaurant.ContactNumber = request.ContactNumber;
        restaurant.Address = CreateAddress(request);

        await repository.UpdateAsync(restaurant, cancellationToken);
    }

    private static Address? CreateAddress(UpdateRestaurantCommand request)
        => request is { City: null, Street: null, PostalCode: null }
            ? null
            : new Address(
                request.City ?? string.Empty,
                request.Street ?? string.Empty,
                request.PostalCode ?? string.Empty);
}

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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
