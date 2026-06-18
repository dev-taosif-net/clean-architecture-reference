using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Domain.Exceptions;

namespace CleanArchitectureReference.Application.Dishes.Queries;

public record GetDishByIdQuery(Guid RestaurantId, int Id) : IQuery<DishDto>;

public class GetDishByIdQueryHandler(IDishRepository dishRepository)
    : IQueryHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(
        GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        var dish = await dishRepository.GetByIdAsync(request.RestaurantId, request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Dish), request.Id);

        return dish.Adapt<DishDto>();
    }
}
