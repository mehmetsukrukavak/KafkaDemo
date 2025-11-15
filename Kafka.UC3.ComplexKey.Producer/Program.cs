using Kafka.UC3.ComplexKey.Producer;

Console.WriteLine("Kafka Producer Use Case 3 With Header!");

var topicName = "use-case-3-complexkey-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendComplexMessageWithComplexKeyAndHeaderAsync(topicName);

Console.WriteLine("Done!");