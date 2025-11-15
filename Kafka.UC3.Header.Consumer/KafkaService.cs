using System.Text;
using Confluent.Kafka;
using Kafka.UC3.Header.Consumer.Events;


namespace Kafka.UC3.Header.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeComplexMessageWithStringKeyAndHeaderAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9094",
            GroupId = "use-case-3-consumer-header-group-1",
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

                var transactionId = Encoding.UTF8.GetString(consumeResult.Message.Headers.First(h => h.Key == "transactionId").GetValueBytes());
                var application = Encoding.UTF8.GetString(consumeResult.Message.Headers.First(h => h.Key == "application").GetValueBytes());
                Console.WriteLine($"Header ->  TransactionId: {transactionId} - Application: {application}");
                
                Console.WriteLine(
                    $"Received Message : Key : {consumeResult.Message.Key} - OrderCode: {orderCreatedEvent.OrderCode} - TotalPrice: {orderCreatedEvent.TotalPrice} - UserId: {orderCreatedEvent.UserId}");
            }

            await Task.Delay(10);
        }
    }
}