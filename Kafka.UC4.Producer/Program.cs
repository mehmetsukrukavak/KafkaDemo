using Kafka.UC4.Producer;

Console.WriteLine("Kafka Producer Use Case 4 Spesific Partition!");

var topicName = "use-case-4-specificpartition-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendMessageToSpecificPartitionAsync(topicName);

Console.WriteLine("Done!");