using Kafka.UC3.ComplexKey.Consumer;

Console.WriteLine("Kafka Consumer Use Case 3 ComplexKey With Header!");

var topicName = "use-case-3-complexkey-topic";
await KafkaService.ConsumeComplexMessageWithComplexKeyKeyAndHeaderAsync(topicName);

Console.ReadLine();