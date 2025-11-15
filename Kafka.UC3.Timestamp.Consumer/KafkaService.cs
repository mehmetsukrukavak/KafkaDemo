using System.Text;
using Confluent.Kafka;
using Kafka.UC3.Timestamp.Consumer.Events;


namespace Kafka.UC3.Timestamp.Consumer;

internal static class KafkaService
{
    internal static async Task ConsumeComplexMessageWithComplexKeyKeyAndHeaderAndTimestampAsync(string topicName)
    {
        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9094",
            GroupId = "use-case-3-consumer-timestamp-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<MessageKey, OrderCreatedEvent>(config)
            .SetValueDeserializer(new CustomValueDeserializer<OrderCreatedEvent>())
            .SetKeyDeserializer(new CustomKeyDeserializer<MessageKey>())
            .Build();

        consumer.Subscribe(topicName);

        while (true)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null)
            {
                var orderCreatedEvent = consumeResult.Message.Value;
                
                var messageKey = consumeResult.Message.Key;
                

                var transactionId = Encoding.UTF8.GetString(consumeResult.Message.Headers.First(h => h.Key == "transactionId").GetValueBytes());
                var application = Encoding.UTF8.GetString(consumeResult.Message.Headers.First(h => h.Key == "application").GetValueBytes());
                Console.WriteLine($"Header ->  TransactionId: {transactionId} - Application: {application}");

                Console.WriteLine($"Timestamp ->  Timestamp: {consumeResult.Message.Timestamp.UtcDateTime}");
                Console.WriteLine(
                    $"Received Message : Key => Key1: {messageKey.Key1} / Key2: {messageKey.Key2} - OrderCode: {orderCreatedEvent.OrderCode} - TotalPrice: {orderCreatedEvent.TotalPrice} - UserId: {orderCreatedEvent.UserId}");
            }

            await Task.Delay(10);
        }
    }
}