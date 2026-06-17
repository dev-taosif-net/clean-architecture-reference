namespace CleanArchitectureReference.Application.Restaurants.Commands;

public record DeleteRestaurantCommand(Guid Id) : ICommand<bool>;

public class DeleteRestaurantCommandHandler(IRestaurantRepository repository)
    : ICommandHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (restaurant is null)
        {
            return false;
        }

        await repository.DeleteAsync(restaurant, cancellationToken);

        return true;
    }
}
