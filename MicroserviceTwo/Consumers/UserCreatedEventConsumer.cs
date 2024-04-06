using MassTransit;
using Shared.Events;

namespace MicroserviceTwo.Consumers
{
    public class UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger) : IConsumer<UserCreatedEvent>
    {
        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            logger.LogInformation($"{context.Message.Email}");

            return Task.CompletedTask;
        }
    }
}