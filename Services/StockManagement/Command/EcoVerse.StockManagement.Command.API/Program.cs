using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Command.Application.Handlers;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using EcoVerse.StockManagement.Command.Infrastructure.Config;
using EcoVerse.StockManagement.Command.Infrastructure.Handlers;
using EcoVerse.StockManagement.Command.Infrastructure.Producers;
using EcoVerse.StockManagement.Command.Infrastructure.Repositories;
using EcoVerse.StockManagement.Command.Infrastructure.Stores;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<InventoryItemAggregate>, EventSourcingHandler>();

builder.Services.AddMediatR(typeof(AddInventoryItemCommandHandler).Assembly);

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