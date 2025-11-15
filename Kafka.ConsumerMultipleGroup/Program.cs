// See https://aka.ms/new-console-template for more information

using Kafka.ConsumerMultipleGroup;

Console.WriteLine("Kafka Consumer Multiple Group!");

var topicName = "use-case-1.1-topic";
await KafkaService.ConsumeSimpleMessageWithNullKeyAsync(topicName);

Console.ReadLine();