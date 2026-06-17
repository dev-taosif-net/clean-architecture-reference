using CleanArchitectureReference.Api.Endpoints;
using CleanArchitectureReference.Api.Handlers;
using CleanArchitectureReference.Application;
using CleanArchitectureReference.Infrastructure.Extensions;
using CleanArchitectureReference.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
// builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.SeedAsync();
}

var apiGroup = app.MapGroup("/api");

apiGroup.MapRestaurantEndpoints();

app.Run();