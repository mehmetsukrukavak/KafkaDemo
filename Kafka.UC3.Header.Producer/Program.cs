// See https://aka.ms/new-console-template for more information

using Kafka.UC3.Header.Producer;

Console.WriteLine("Kafka Producer Use Case 3 With Header!");

var topicName = "use-case-3-header-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendComplexMessageWithStringKeyAndHeaderAsync(topicName);

Console.WriteLine("Done!");