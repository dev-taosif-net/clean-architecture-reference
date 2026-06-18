using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;

namespace CleanArchitectureReference.Application.Dishes.Commands;

public record DeleteDishCommand(Guid RestaurantId, int Id) : ICommand;

public class DeleteDishCommandHandler(IDishRepository dishRepository)
    : ICommandHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await dishRepository.GetByIdAsync(request.RestaurantId, request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Dish), request.Id);

        await dishRepository.DeleteAsync(dish, cancellationToken);
    }
}
