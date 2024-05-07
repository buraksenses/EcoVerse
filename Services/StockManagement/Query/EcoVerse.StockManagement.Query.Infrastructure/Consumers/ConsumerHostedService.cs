using CQRS.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EcoVerse.StockManagement.Query.Infrastructure.Consumers;

public class ConsumerHostedService : IHostedService
{
    private readonly ILogger<ConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Task _executingTask;
    private CancellationTokenSource _cts;

    public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event consumer service running.");

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

        // Task.Run içinde her seferinde yeni bir scope oluştur
        _executingTask = Task.Run(async () => 
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                    eventConsumer.Consume(topic);
                }
                await Task.Delay(1000, _cts.Token);  // Örneğin, her 1 saniyede bir consume işlemi
            }
        }, _cts.Token);

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_executingTask == null)
        {
            return;
        }

        try
        {
            _cts.Cancel();
        }
        finally
        {
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
}
