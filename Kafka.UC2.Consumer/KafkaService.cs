using Confluent.Kafka;

namespace Kafka.UC2.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeSimpleMessageWithIntKeyAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9094",
            GroupId = "use-case-2-consumer-group-1",
            AutoOffsetReset = AutoOffsetReset.Latest
        };
        
        var consumer = new ConsumerBuilder<int, string>(config).Build();
        consumer.Subscribe(topicName);

        while (true)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null) Console.WriteLine($"Received Message : Key : {consumeResult.Message.Key} Value : {consumeResult.Message.Value} ");
            
            await Task.Delay(500);
        }
        
        
    }
}