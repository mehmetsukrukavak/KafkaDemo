using Confluent.Kafka;

namespace Kafka.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeSimpleMessageWithNullKeyAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9094",
            GroupId = "use-case-1-consumer-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        var consumer = new ConsumerBuilder<Null, string>(config).Build();
        consumer.Subscribe(topicName);

        while (true)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null) Console.WriteLine($"Received Message : {consumeResult.Message.Value} ");
            
            await Task.Delay(500);
        }
        
        
    }
}