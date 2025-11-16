using Order.API.Dtos;
using Order.API.Services.interfaces;
using RealWorld.Shared;
using RealWorld.Shared.Events;

namespace Order.API.Services;

public class OrderService(IBus bus)
{
    public async Task<bool> CreateOrderAsync(OrderCreateRequestDto request)
    {
        var orderCode = Guid.NewGuid().ToString();
        //Save to database
        var orderCreatedEvent = new OrderCreatedEvent(orderCode, request.TotalPrice, request.UserId);

        return await bus.PublishAsync(orderCode, orderCreatedEvent,BusConstants.OrderCreatedEventTopicName);
    }
}