using CleanArchitectureReference.Domain.Entities;

namespace CleanArchitectureReference.Application.Restaurants.Queries;

public record GetAllRestaurantsQuery : IQuery<IReadOnlyList<RestaurantDto>>;

public class GetAllRestaurantsQueryHandler(IRestaurantRepository repository)
    : IQueryHandler<GetAllRestaurantsQuery, IReadOnlyList<RestaurantDto>>
{
    private static readonly TypeAdapterConfig MappingConfig = CreateMappingConfig();

    public async Task<IReadOnlyList<RestaurantDto>> Handle(
        GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await repository.GetAllAsync(cancellationToken);

        return restaurants.Adapt<IReadOnlyList<RestaurantDto>>(MappingConfig);
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