using CleanArchitectureReference.Api.Endpoints;
using CleanArchitectureReference.Domain.Entities;
using CleanArchitectureReference.Infrastructure.Persistence;
using CleanArchitectureReference.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitectureReference.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var apiGroup = app.MapGroup("/api");

        apiGroup.MapRestaurantEndpoints();
        apiGroup.MapDishEndpoints();

        apiGroup.MapGroup("/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        return app;
    }

    public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        await authDbContext.Database.MigrateAsync();

        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
        await seeder.SeedAsync();

        return app;
    }
}
