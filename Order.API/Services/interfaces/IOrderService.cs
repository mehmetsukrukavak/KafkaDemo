using Order.API.Dtos;

namespace Order.API.Services.interfaces;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderCreateRequestDto request);
}