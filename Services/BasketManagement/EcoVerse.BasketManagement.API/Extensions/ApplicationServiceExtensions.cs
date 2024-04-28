﻿using System.IdentityModel.Tokens.Jwt;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.BasketManagement.Application.Services;
using EcoVerse.BasketManagement.Application.Validations;
using EcoVerse.BasketManagement.Domain.Interfaces;
using EcoVerse.BasketManagement.Infrastructure.Repositories;
using EcoVerse.BasketManagement.Infrastructure.Services;
using EcoVerse.BasketManagement.Infrastructure.Settings;
using EcoVerse.Shared.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

namespace EcoVerse.BasketManagement.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
        services.AddControllers(opt =>
        {
            opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
        });
        
        services.AddControllers(opt =>
        {
            opt.Filters.Add(new AuthorizeFilter());
        });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<RedisSettings>(config.GetSection("RedisSettings"));
        services.AddSingleton<RedisService>(sp =>
        {
            var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
            var redis = new RedisService(redisSettings.Host, redisSettings.Port);
            redis.Connect();
            return redis;
        });

        services.AddHttpContextAccessor();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();
        
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.Authority = config["IdentityServerURL"];
            options.Audience = "resource_basket";
            options.RequireHttpsMetadata = false;
        });

        services.AddFluentValidation(fv => 
            fv.RegisterValidatorsFromAssemblyContaining<AddToCartValidator>());
        
        return services;
    }
}