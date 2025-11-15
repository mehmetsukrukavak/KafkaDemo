using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.UC3.Producer.Events;

namespace Kafka.UC3.Producer;

internal static class KafkaService
{
    internal static async Task CreateTopicAsync(string topicName)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig()
        {
            BootstrapServers = "localhost:9094"
        }).Build();
        try
        {
            await adminClient.CreateTopicsAsync(new[]
            {
                new TopicSpecification()
                {
                    Name = topicName,
                    NumPartitions = 3,
                    ReplicationFactor = 1
                }
            });
            Console.WriteLine($"Topic {topicName} created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Topic already exists - {ex.Message}");
        }
    }

    internal static async Task SendComplexMessageWithStringKeyAsync(string topicName)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9094"
        };

        using var producer = new ProducerBuilder<string, OrderCreatedEvent>(config)
            .SetValueSerializer(new CustomSerializer<OrderCreatedEvent>())
            .Build();

        foreach (var item in Enumerable.Range(1, 100))
        {
            var orderCreatedEvent = new OrderCreatedEvent()
            {
                OrderCode = Guid.NewGuid().ToString(),
                TotalPrice = item * 100,
                UserId = item
            };
            var message = new Message<string, OrderCreatedEvent>()
            {
                Value = orderCreatedEvent,
                Key = Guid.NewGuid().ToString()
            };

            var result = await producer.ProduceAsync(topicName, message);

            foreach (var propertyInfo in result.GetType().GetProperties())
            {
                Console.WriteLine($"{propertyInfo.Name}: {propertyInfo.GetValue(result)} ");
            }

            Console.WriteLine("--------------------------------------");
            await Task.Delay(10);
        }
    }
}