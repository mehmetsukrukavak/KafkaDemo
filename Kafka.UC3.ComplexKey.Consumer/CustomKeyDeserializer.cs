using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.UC3.ComplexKey.Consumer;

internal class CustomKeyDeserializer<T>:IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}