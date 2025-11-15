using Kafka.UC5.Consumer;

Console.WriteLine("Kafka Consumer Use Case 5 Specific Offset!");

var topicName = "use-case-5-specificoffset-topic";
await KafkaService.ConsumeMessageFromSpecificPartitionAndAfterSpecificOffsetAsync(topicName);
