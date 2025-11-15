using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.UC3.ComplexKey.Producer.Events;


namespace Kafka.UC3.ComplexKey.Producer;

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

    internal static async Task SendComplexMessageWithComplexKeyAndHeaderAsync(string topicName)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9094"
        };

        using var producer = new ProducerBuilder<MessageKey, OrderCreatedEvent>(config)
            .SetValueSerializer(new CustomSerializer<OrderCreatedEvent>())
            .SetKeySerializer(new CustomKeySerializer<MessageKey>())
            .Build();
        
        Random rnd = new Random();
        foreach (var item in Enumerable.Range(1, 100))
        {
            var orderCreatedEvent = new OrderCreatedEvent()
            {
                OrderCode = Guid.NewGuid().ToString(),
                TotalPrice = item * 100,
                UserId = item
            };
            var transactionId = rnd.Next(50000).ToString().PadLeft(5, '0');

            var header = new Headers
            {
                { "transactionId", Encoding.UTF8.GetBytes(transactionId) },
                { "application", Encoding.UTF8.GetBytes("v1.0.0") }
            };

            var message = new Message<MessageKey, OrderCreatedEvent>()
            {
                Value = orderCreatedEvent,
                Key = new MessageKey(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                Headers = header
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