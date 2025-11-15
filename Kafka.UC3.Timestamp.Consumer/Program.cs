using Kafka.UC3.Timestamp.Consumer;

Console.WriteLine("Kafka Consumer Use Case 3 ComplexKey With Header With Timestamp!");

var topicName = "use-case-3-timestamp-topic";
await KafkaService.ConsumeComplexMessageWithComplexKeyKeyAndHeaderAndTimestampAsync(topicName);
