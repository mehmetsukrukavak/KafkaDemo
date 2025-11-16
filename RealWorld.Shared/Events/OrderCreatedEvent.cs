namespace RealWorld.Shared.Events;

public record OrderCreatedEvent(string OrderCode, decimal TotalPrice, int UserId);