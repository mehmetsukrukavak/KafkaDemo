using Kafka.Ack.Producer;

Console.WriteLine("Kafka Producer with Acknowledgement!");

var topicName = "ack-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendMessageToWithAckAsync(topicName);

Console.WriteLine("Done!");