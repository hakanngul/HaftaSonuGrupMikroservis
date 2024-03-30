namespace Producer.App.Events
{
    internal record UserCreatedEvent
    {
        public int Id { get; init; }
        public string Email { get; init; } = default!;
    }
}