using Kafka.Cluster.Producer;

Console.WriteLine("Kafka Producer - Retry!");
//https://docs.confluent.io
var topicName = "retry-topic";
await KafkaService.CreateTopicRetryWithClusterAsync(topicName);
await KafkaService.SendMessageWithRetryToClusterAsync(topicName);

Console.WriteLine("Done!");