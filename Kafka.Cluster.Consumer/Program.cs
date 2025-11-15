using Kafka.Cluster.Consumer;

Console.WriteLine("Kafka Consumer - Cluster!");
//https://docs.confluent.io
var topicName = "cluster-topic";
await KafkaService.ConsumeMessageFromClusterAsync(topicName);