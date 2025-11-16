using Confluent.Kafka;
using Stock.API.Services.interfaces;


namespace Stock.API.Services;

public class Bus(IConfiguration configuration) : IBus
{
    public ConsumerConfig GetConsumerConfig(string groupId)
    {
        return new ()
        {
            BootstrapServers =  configuration.GetSection("BusSettings").GetSection("Kafka")["BootstrapServers"],
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
    }
}