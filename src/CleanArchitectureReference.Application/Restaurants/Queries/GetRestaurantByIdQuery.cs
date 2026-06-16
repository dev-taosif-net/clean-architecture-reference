using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Application.Restaurants.Queries;

public record GetRestaurantByIdQuery(Guid Id) : IQuery<RestaurantDto?>;

public class GetRestaurantByIdQueryHandler(IRestaurantRepository repository)
    : IQueryHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    private static readonly TypeAdapterConfig MappingConfig = CreateMappingConfig();

    public async Task<RestaurantDto?> Handle(
        GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id, cancellationToken);

        return restaurant?.Adapt<RestaurantDto>(MappingConfig);
    }

    private static TypeAdapterConfig CreateMappingConfig()
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<Restaurant, RestaurantDto>()
            .Map(dest => dest.City, src => src.Address!.City)
            .Map(dest => dest.Street, src => src.Address!.Street)
            .Map(dest => dest.PostalCode, src => src.Address!.PostalCode);

        return config;
    }
}
