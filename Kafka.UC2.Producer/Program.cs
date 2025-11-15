// See https://aka.ms/new-console-template for more information

using Kafka.UC2.Producer;

Console.WriteLine("Kafka Producer Use Case 2!");

var topicName = "use-case-2-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendSimpleMessageWithIntKeyAsync(topicName);

Console.WriteLine("Done!");
