using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Admin;


namespace Kafka.Cluster.Producer;

internal static class KafkaService
{
    internal static async Task CreateTopicWithClusterAsync(string topicName)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig()
        {
            BootstrapServers = "localhost:7001,localhost:7002,localhost:7003",
        }).Build();
        try
        {
            var configs = new Dictionary<string, string>()
            {
                { "message.timestamp.type", "LogAppendTime" }, //Default is CreateTime
                { "retention.ms", "-1" } //No retention policy
            };
            await adminClient.CreateTopicsAsync(new[]
            {
                new TopicSpecification()
                {
                    Name = topicName,
                    NumPartitions = 6,
                    ReplicationFactor = 3,
                    Configs = configs
                }
            });
            Console.WriteLine($"Topic {topicName} created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Topic already exists - {ex.Message}");
        }
    }
    
    internal static async Task SendSimpleMessageToClusterAsync(string topicName)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:7001,localhost:7002,localhost:7003"
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        foreach (var item in Enumerable.Range(1, 10))
        {
            var message = new Message<Null, string>()
            {
                Value = $"Message (cluster) - {item}"
            };

            var result = await producer.ProduceAsync(topicName, message);

            foreach (var propertyInfo in result.GetType().GetProperties())
            {
                Console.WriteLine($"{propertyInfo.Name}: {propertyInfo.GetValue(result)} ");
                
            }

            Console.WriteLine("--------------------------------------");
            await Task.Delay(200);
        }
    }
}