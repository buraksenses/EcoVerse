using EcoVerse.ProductManagement.Infrastructure.Configurations;

namespace EcoVerse.ProductManagement.API.Extensions;

public static class ApplicationServiceExtensions
{
     public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        ProductDbContextConfiguration.ConfigureDbContext(services, config);
        
        return services;
    }
}