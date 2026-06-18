using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;

namespace CleanArchitectureReference.Application.Restaurants.Commands;

public record DeleteRestaurantCommand(Guid Id) : ICommand;

public class DeleteRestaurantCommandHandler(IRestaurantRepository repository)
    : ICommandHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id);

        await repository.DeleteAsync(restaurant, cancellationToken);
    }
}
