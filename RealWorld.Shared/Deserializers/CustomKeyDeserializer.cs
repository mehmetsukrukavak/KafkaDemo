using System.Text.Json;
using Confluent.Kafka;

namespace RealWorld.Shared.Deserializers;
public class CustomKeyDeserializer<T>:IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}