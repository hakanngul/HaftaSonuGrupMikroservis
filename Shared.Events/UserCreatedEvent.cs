namespace Shared.Events
{
    public record UserCreatedEvent
    {
        public Guid UserId { get; init; }
        public string Phone { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}