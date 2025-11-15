namespace Kafka.UC3.Timestamp.Producer.Events;

internal record OrderCreatedEvent
{
    public string OrderCode { get; init; } = default!;
    public decimal TotalPrice { get; init; }
    public int UserId { get; init; }

}