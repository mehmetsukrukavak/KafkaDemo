using Kafka.UC4.Consumer;

Console.WriteLine("Kafka Consumer Use Case 4 Specific Paritition!");

var topicName = "use-case-4-specificpartition-topic";
await KafkaService.ConsumeMessageFromSpecificPartitionAsync(topicName);
