using Kafka.Ack.Consumer;

Console.WriteLine("Kafka Consumer with Acknowledgement!!");

var topicName = "ack-topic";
await KafkaService.ConsumeMessageWithAckAsync(topicName);
