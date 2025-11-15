using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.UC3.Header.Producer;

internal class CustomSerializer<T>:ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, typeof(T)));
    }
}