using System.Diagnostics;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Admin;


namespace Kafka.Cluster.Producer;

internal static class KafkaService
{
    internal static async Task CreateTopicRetryWithClusterAsync(string topicName)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig()
        {
            BootstrapServers = "localhost:7001,localhost:7002,localhost:7003",
            Acks = Acks.All
            
        }).Build();
        try
        {
            var configs = new Dictionary<string, string>()
            {
                { "message.timestamp.type", "LogAppendTime" }, //Default is CreateTime
                { "retention.ms", "-1" }, //No retention policy
                { "min.insync.replicas", "3" }
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
    
    internal static async Task SendMessageWithRetryToClusterAsync(string topicName)
    {
        // Task.Run(async () => {
        //     Stopwatch stopwatch = new Stopwatch();
        //     stopwatch.Start();
        //     while (true)
        //     {
        //         TimeSpan timeSpan = TimeSpan.FromSeconds(Convert.ToInt32(stopwatch.Elapsed.TotalSeconds));
        //         Console.Write(timeSpan.ToString("c"));
        //         Console.Write("\r");
        //         await Task.Delay(1000);
        //     }
        // });
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:7001,localhost:7002,localhost:7003",
            Acks = Acks.All,
            //MessageSendMaxRetries = 5
            MessageTimeoutMs = 5000 //instead of MessageSendMaxRetries
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