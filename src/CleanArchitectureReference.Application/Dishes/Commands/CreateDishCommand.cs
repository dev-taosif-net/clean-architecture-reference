using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;
using FluentValidation;

namespace CleanArchitectureReference.Application.Dishes.Commands;

public record CreateDishCommand(
    Guid RestaurantId,
    string Name,
    string Description,
    decimal Price,
    int? KiloCalories) : ICommand<int>;

public class CreateDishCommandHandler(
    IDishRepository dishRepository,
    IRestaurantRepository restaurantRepository)
    : ICommandHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _ = await restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId);

        var dish = new Dish
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            KiloCalories = request.KiloCalories,
            RestaurantId = request.RestaurantId,
        };

        await dishRepository.AddAsync(dish, cancellationToken);

        return dish.Id;
    }
}

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .When(x => x.KiloCalories.HasValue);
    }
}
