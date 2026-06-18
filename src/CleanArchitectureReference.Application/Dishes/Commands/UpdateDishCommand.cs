using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;
using FluentValidation;

namespace CleanArchitectureReference.Application.Dishes.Commands;

public record UpdateDishCommand(
    Guid RestaurantId,
    int Id,
    string Name,
    string Description,
    decimal Price,
    int? KiloCalories) : ICommand;

public class UpdateDishCommandHandler(IDishRepository dishRepository)
    : ICommandHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await dishRepository.GetByIdAsync(request.RestaurantId, request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Dish), request.Id);

        dish.Name = request.Name;
        dish.Description = request.Description;
        dish.Price = request.Price;
        dish.KiloCalories = request.KiloCalories;

        await dishRepository.UpdateAsync(dish, cancellationToken);
    }
}

public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
{
    public UpdateDishCommandValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty();

        RuleFor(x => x.Id)
            .GreaterThan(0);

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
