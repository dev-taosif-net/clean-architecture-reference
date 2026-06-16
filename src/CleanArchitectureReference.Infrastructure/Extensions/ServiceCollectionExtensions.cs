using CleanArchitectureReference.Infrastructure.Persistence;
using CleanArchitectureReference.Infrastructure.Seeders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureReference.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    
            return services;
        }
}