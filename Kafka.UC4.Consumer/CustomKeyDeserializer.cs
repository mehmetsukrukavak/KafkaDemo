using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.UC4.Consumer;

internal class CustomKeyDeserializer<T>:IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}