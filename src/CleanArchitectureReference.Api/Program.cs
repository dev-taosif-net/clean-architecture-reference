using CleanArchitectureReference.Api;
using CleanArchitectureReference.Api.Extensions;
using CleanArchitectureReference.Application;
using CleanArchitectureReference.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddSerilogLogging();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();

    var app = builder.Build();

    app.UsePresentation();

    await app.SeedDatabaseAsync();

    app.MapEndpoints();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
