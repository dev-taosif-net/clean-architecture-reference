using CleanArchitectureReference.Application.Dishes.Commands;
using CleanArchitectureReference.Application.Dishes.Queries;
using MediatR;

namespace CleanArchitectureReference.Api.Endpoints;

public static class DishEndpoints
{
    public static IEndpointRouteBuilder MapDishEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/restaurants/{restaurantId:guid}/dishes")
            .WithTags("Dishes");

        group.MapGet("/", async (
                Guid restaurantId, ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetDishesByRestaurantQuery(restaurantId), cancellationToken)))
            .WithName("GetDishesByRestaurant");

        group.MapGet("/{dishId:int}", async (
                Guid restaurantId, int dishId, ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetDishByIdQuery(restaurantId, dishId), cancellationToken)))
            .WithName("GetDishById");

        group.MapPost("/", async (
                Guid restaurantId, CreateDishCommand command, ISender sender, CancellationToken cancellationToken) =>
            {
                var id = await sender.Send(command with { RestaurantId = restaurantId }, cancellationToken);

                return Results.CreatedAtRoute("GetDishById", new { restaurantId, dishId = id }, new { id });
            })
            .WithName("CreateDish");

        group.MapPut("/{dishId:int}", async (
                Guid restaurantId, int dishId, UpdateDishCommand command, ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(command with { RestaurantId = restaurantId, Id = dishId }, cancellationToken);

                return Results.NoContent();
            })
            .WithName("UpdateDish");

        group.MapDelete("/{dishId:int}", async (
                Guid restaurantId, int dishId, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteDishCommand(restaurantId, dishId), cancellationToken);

                return Results.NoContent();
            })
            .WithName("DeleteDish");

        return app;
    }
}
