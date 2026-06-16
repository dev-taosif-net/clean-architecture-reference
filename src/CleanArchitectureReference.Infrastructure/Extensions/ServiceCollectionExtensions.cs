using CleanArchitectureReference.Application.Common.Abstractions;
using CleanArchitectureReference.Infrastructure.Common;
using CleanArchitectureReference.Infrastructure.Persistence;
using CleanArchitectureReference.Infrastructure.Persistence.Interceptors;
using CleanArchitectureReference.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureReference.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
                options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>()));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            return services;
        }
}