using Order.API.Services.interfaces;
using RealWorld.Shared.Events;

namespace Order.API.Services;

public static class BusExtension
{
    public static async Task CreatetopicOrQueueAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var bus = scope.ServiceProvider.GetRequiredService<IBus>();

        await bus.CreateTopicOrQueueAsync(new List<string>()
        {
            BusConstants.OrderCreatedEventTopicName
        });
    }
}