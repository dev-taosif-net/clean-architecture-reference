using CleanArchitectureReference.Api.Endpoints;
using CleanArchitectureReference.Application;
using CleanArchitectureReference.Infrastructure.Extensions;
using CleanArchitectureReference.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.SeedAsync();
}

app.MapRestaurantEndpoints();

app.Run();