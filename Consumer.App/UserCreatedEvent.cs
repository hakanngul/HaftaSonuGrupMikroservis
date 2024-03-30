namespace Consumer.App
{
    internal record UserCreatedEvent
    {
        public int Id { get; init; }
        public string Email { get; init; } = default!;
    }
}