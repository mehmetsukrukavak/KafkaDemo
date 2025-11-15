using Confluent.Kafka;

namespace Kafka.Cluster.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeMessageFromClusterAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:7001,localhost:7002,localhost:7003",
            GroupId = "cluster-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        
        var consumer = new ConsumerBuilder<Null, string>(config).Build();
        consumer.Subscribe(topicName);

        while (true)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null)
                try
                {
                    Console.WriteLine($"Received Message : {consumeResult.Message.Value} ");
                    consumer.Commit(consumeResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            
            await Task.Delay(500);
        }
        
        
    }
}