

using Kafka.Producer;

Console.WriteLine("Kafka Producer!");

/*var topicName = "use-case-1-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendSimpleMessageWithNullKeyAsync(topicName);*/

var topicName = "use-case-1.1-topic";
await KafkaService.CreateTopicAsync(topicName);
await KafkaService.SendSimpleMessageWithNullKeyAsync(topicName);

Console.WriteLine("Done!");
    
    
