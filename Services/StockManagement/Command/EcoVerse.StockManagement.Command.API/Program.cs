using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Command.Application.Consumers;
using EcoVerse.StockManagement.Command.Application.Handlers;
using EcoVerse.StockManagement.Command.Application.Validations;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using EcoVerse.StockManagement.Command.Infrastructure.Config;
using EcoVerse.StockManagement.Command.Infrastructure.Handlers;
using EcoVerse.StockManagement.Command.Infrastructure.Producers;
using EcoVerse.StockManagement.Command.Infrastructure.Repositories;
using EcoVerse.StockManagement.Command.Infrastructure.Stores;
using EcoVerse.StockManagement.Common.Events;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApplicationServices(builder.Configuration);

BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<InventoryItemAddedEvent>();
BsonClassMap.RegisterClassMap<InventoryItemRemovedEvent>();
BsonClassMap.RegisterClassMap<InventoryItemQuantityUpdatedEvent>();
BsonClassMap.RegisterClassMap<InventoryItemPriceUpdatedEvent>();

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AddNewProductEventConsumer>();
    x.AddConsumer<StockCheckResponseEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("new-product-added-queue", e =>
        {
            e.ConfigureConsumer<AddNewProductEventConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("add-to-cart-queue", e =>
        {
            e.ConfigureConsumer<StockCheckResponseEventConsumer>(context);
        });
                
        cfg.ConfigureEndpoints(context);
               
    });
});

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<InventoryItemAggregate>, EventSourcingHandler>();

builder.Services.AddMediatR(typeof(AddInventoryItemCommandHandler).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddInventoryItemValidator>();

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