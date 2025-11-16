using Confluent.Kafka;

namespace Stock.API.Services.interfaces;

public interface IBus
{
    ConsumerConfig GetConsumerConfig(string groupId);
}