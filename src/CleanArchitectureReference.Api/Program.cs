using CleanArchitectureReference.Api.Endpoints;
using CleanArchitectureReference.Api.Handlers;
using CleanArchitectureReference.Application;
using CleanArchitectureReference.Infrastructure;
using CleanArchitectureReference.Infrastructure.Seeders;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseExceptionHandler(options => { });

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
    apiGroup.MapDishEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}