using Kafka.UC3.Consumer;

Console.WriteLine("Kafka Consumer Use Case 3!");

var topicName = "use-case-3-topic";
await KafkaService.ConsumeComplexMessageWithStringKeyAsync(topicName);

Console.ReadLine();