using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Order.API.Services.interfaces;
using RealWorld.Shared.Serializers;

namespace Order.API.Services;

public class Bus(IConfiguration configuration, ILogger<Bus> logger) : IBus
{
    private readonly ProducerConfig _config = new()
    {
        BootstrapServers = configuration.GetSection("BusSettings").GetSection("Kafka")["BootstrapServers"],
        Acks = Acks.All,
        MessageTimeoutMs = 5000,
        AllowAutoCreateTopics = true
    };

    public async Task<bool> PublishAsync<T1, T2>(T1 key, T2 value, string topicOrQueueName)
    {
        using var producer = new ProducerBuilder<T1, T2>(_config)
            .SetKeySerializer(new CustomKeySerializer<T1>())
            .SetValueSerializer(new CustomValueSerializer<T2>())
            .Build();

        var message = new Message<T1, T2>()
        {
            Key = key,
            Value = value
        };

        var result = await producer.ProduceAsync(topicOrQueueName, message);

        return result.Status == PersistenceStatus.Persisted;
    }

    public Task CreateTopicOrQueueAsync(List<string> topicOrQueueNameList)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig()
        {
            BootstrapServers = configuration.GetSection("BusSettings").GetSection("Kafka")["BootstrapServers"]
        }).Build();

        topicOrQueueNameList.ForEach(async topicOrQueueName =>
        {
            try
            {
                await adminClient.CreateTopicsAsync(new[]
                {
                    new TopicSpecification()
                    {
                        Name = topicOrQueueName,
                        NumPartitions = 6,
                        ReplicationFactor = 1
                    }
                });
                logger.LogInformation($"Topic ({topicOrQueueName}) created successfully");
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Topic already exists - {ex.Message}");
            }
        });


        return Task.CompletedTask;
    }
}