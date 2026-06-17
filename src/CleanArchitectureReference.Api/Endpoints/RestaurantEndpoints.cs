using CleanArchitectureReference.Application.Restaurants.Commands;
using CleanArchitectureReference.Application.Restaurants.Queries;
using MediatR;

namespace CleanArchitectureReference.Api.Endpoints;

public static class RestaurantEndpoints
{
    public static IEndpointRouteBuilder MapRestaurantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/restaurants")
            .WithTags("Restaurants");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetAllRestaurantsQuery(), cancellationToken)))
            .WithName("GetAllRestaurants");

        group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var restaurant = await sender.Send(new GetRestaurantByIdQuery(id), cancellationToken);

                return restaurant is null ? Results.NotFound() : Results.Ok(restaurant);
            })
            .WithName("GetRestaurantById");

        group.MapPost("/", async (
                CreateRestaurantCommand command, ISender sender, CancellationToken cancellationToken) =>
            {
                var id = await sender.Send(command, cancellationToken);

                return Results.CreatedAtRoute("GetRestaurantById", new { id }, new { id });
            })
            .WithName("CreateRestaurant");

        group.MapPut("/{id:guid}", async (
                Guid id, UpdateRestaurantCommand command, ISender sender, CancellationToken cancellationToken) =>
            {
                var updated = await sender.Send(command with { Id = id }, cancellationToken);

                return updated ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateRestaurant");

        group.MapDelete("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var deleted = await sender.Send(new DeleteRestaurantCommand(id), cancellationToken);

                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteRestaurant");

        return app;
    }
}