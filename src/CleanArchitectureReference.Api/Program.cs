using CleanArchitectureReference.Infrastructure.Extensions;
using CleanArchitectureReference.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.SeedAsync();

app.MapGet("/", () => "Hello World!");

app.Run();