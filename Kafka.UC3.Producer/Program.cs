// See https://aka.ms/new-console-template for more information

using Kafka.UC3.Producer;

Console.WriteLine("Kafka Producer Use Case 3!");

var topicName = "use-case-3-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendComplexMessageWithStringKeyAsync(topicName);

Console.WriteLine("Done!");