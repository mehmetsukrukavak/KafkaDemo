using Confluent.Kafka;
using Kafka.UC3.Consumer.Events;

namespace Kafka.UC3.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeComplexMessageWithStringKeyAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9094",
            GroupId = "use-case-3-consumer-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, OrderCreatedEvent>(config)
            .SetValueDeserializer(new CustomValueDeserializer<OrderCreatedEvent>())
            .Build();

        consumer.Subscribe(topicName);

        while (true)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null)
            {
                var orderCreatedEvent = consumeResult.Message.Value;

                Console.WriteLine(
                    $"Received Message : Key : {consumeResult.Message.Key} - OrderCode: {orderCreatedEvent.OrderCode} - TotalPrice: {orderCreatedEvent.TotalPrice} - UserId: {orderCreatedEvent.UserId}");
            }

            await Task.Delay(10);
        }
    }
}