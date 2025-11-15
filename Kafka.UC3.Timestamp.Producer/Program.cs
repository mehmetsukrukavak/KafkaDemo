using Kafka.UC3.Timestamp.Producer;

Console.WriteLine("Kafka Producer Use Case 3 With Header and Timestamp!");

var topicName = "use-case-3-timestamp-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendComplexMessageWithComplexKeyAndHeaderAndTimestampAsync(topicName);

Console.WriteLine("Done!");