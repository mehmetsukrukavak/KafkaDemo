using Kafka.UC2.Consumer;

Console.WriteLine("Kafka Consumer Use Case 2!");

var topicName = "use-case-2-topic";
await KafkaService.ConsumeSimpleMessageWithIntKeyAsync(topicName);

Console.ReadLine();