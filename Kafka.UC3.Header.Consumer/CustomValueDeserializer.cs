using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.UC3.Header.Consumer;

internal class CustomValueDeserializer<T>:IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}