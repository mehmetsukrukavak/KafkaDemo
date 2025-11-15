using Kafka.Cluster.Producer;

Console.WriteLine("Kafka Producer - Cluster!");
//https://docs.confluent.io
var topicName = "cluster-topic";
await KafkaService.CreateTopicWithClusterAsync(topicName);
await KafkaService.SendSimpleMessageToClusterAsync(topicName);

Console.WriteLine("Done!");