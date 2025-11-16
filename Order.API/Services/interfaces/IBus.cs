namespace Order.API.Services.interfaces;

public interface IBus
{
    Task<bool> PublishAsync<T1, T2>(T1 key, T2 value, string topicOrQueueName);
    Task CreateTopicOrQueueAsync(List<string> topicOrQueueNameList);
}