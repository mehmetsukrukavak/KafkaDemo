using Kafka.UC5.Producer;

Console.WriteLine("Kafka Producer Use Case 5 Spesific Offset!");

var topicName = "use-case-5-specificoffset-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendMessageToSpecificPartitionAsync(topicName);

Console.WriteLine("Done!");