using Confluent.Kafka;
using CQRS.Core.Consumers;
using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Query.Application.Consumers;
using EcoVerse.StockManagement.Query.Application.Handlers;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using EcoVerse.StockManagement.Query.Infrastructure.Data;
using EcoVerse.StockManagement.Query.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApplicationServices(builder.Configuration);

Action<DbContextOptionsBuilder> configureDbContext = 
    o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.AddDbContext<StockReadDbContext>(configureDbContext);
builder.Services.AddSingleton(new DatabaseContextFactory(configureDbContext));

builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

builder.Services.AddHostedService<ConsumerHostedService>();

builder.Services.AddMediatR(typeof(InventoryItemAddedEventHandler).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();