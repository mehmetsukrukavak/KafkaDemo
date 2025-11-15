using Kafka.Ack.Producer;

Console.WriteLine("Kafka Producer with Acknowledgement!");
//https://docs.confluent.io
var topicName = "ack-topic"; 
topicName = "retention-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendMessageToWithAckAsync(topicName);

Console.WriteLine("Done!");