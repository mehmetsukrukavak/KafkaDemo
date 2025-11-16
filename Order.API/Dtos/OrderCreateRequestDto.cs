namespace Order.API.Dtos;

public record OrderCreateRequestDto(decimal TotalPrice, int UserId);