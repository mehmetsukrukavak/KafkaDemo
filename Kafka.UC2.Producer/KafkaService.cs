using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Kafka.UC2.Producer;

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

    internal static async Task SendSimpleMessageWithIntKeyAsync(string topicName)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9094"
        };

        using var producer = new ProducerBuilder<int, string>(config).Build();

        foreach (var item in Enumerable.Range(1, 10))
        {
            var message = new Message<int, string>()
            {
                Value = $"Message (use-case-2) - {item}",
                Key = item
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