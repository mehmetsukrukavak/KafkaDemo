using Kafka.UC3.Header.Consumer;

Console.WriteLine("Kafka Consumer Use Case 3 With Header!");

var topicName = "use-case-3-header-topic";
await KafkaService.ConsumeComplexMessageWithStringKeyAndHeaderAsync(topicName);

Console.ReadLine();