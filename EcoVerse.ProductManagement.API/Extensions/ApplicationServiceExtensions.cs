using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Services;
using EcoVerse.ProductManagement.Application.Validations.Product;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.ProductManagement.Infrastructure.Configurations;
using EcoVerse.ProductManagement.Infrastructure.Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace EcoVerse.ProductManagement.API.Extensions;

public static class ApplicationServiceExtensions
{
     public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add(new AuthorizeFilter());
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.Authority = config["IdentityServerURL"];
            options.Audience = "resource_catalog";
            options.RequireHttpsMetadata = false;
        });
        
        ProductDbContextConfiguration.ConfigureDbContext(services, config);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        services.AddFluentValidation(fv => 
            fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());
        
        return services;
    }
}