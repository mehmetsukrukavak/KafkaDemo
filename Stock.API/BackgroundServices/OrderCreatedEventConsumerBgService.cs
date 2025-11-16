using Confluent.Kafka;
using RealWorld.Shared;
using RealWorld.Shared.Deserializers;
using RealWorld.Shared.Events;

using Stock.API.Services.interfaces;

namespace Stock.API.BackgroundServices;

public class OrderCreatedEventConsumerBgService(
    IBus bus,
    ILogger<OrderCreatedEventConsumerBgService> logger
    ) : BackgroundService
{
    private IConsumer<string, OrderCreatedEvent>? _consumer;

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer =
            new ConsumerBuilder<string, OrderCreatedEvent>(
                    bus.GetConsumerConfig(BusConstants.OrderCreatedEventTopicGroupId))
                .SetValueDeserializer(new CustomValueDeserializer<OrderCreatedEvent>())
                .Build();
        _consumer.Subscribe(BusConstants.OrderCreatedEventTopicName);
        
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = _consumer!.Consume(5000);

            if (consumeResult != null)
            {
                try
                {
                    var orderCreatedEvent = consumeResult.Message.Value;
                    //decrease from stock

                    logger.LogInformation(
                        $"Received Message : Key : {consumeResult.Message.Key} - OrderCode: {orderCreatedEvent.OrderCode} - TotalPrice: {orderCreatedEvent.TotalPrice} - UserId: {orderCreatedEvent.UserId}");
                    
                    _consumer.Commit(consumeResult);
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    throw;
                }
                
            }

            await Task.Delay(10, stoppingToken);
        }
    }
}