namespace Infrastructure.Entities;

public class EventEntity : Entity
{
    public Guid CreatorId { get; init; }
    
    public string Title { get; init; }
    
    public DateTime StartDateTime { get; init; }
    
    public DateTime EndDateTime { get; init; }
    
    public Guid? RepeatId { get; init; }
    
    public string? Description { get; init; }
    
    public Guid[]? GuestIds { get; init; }
}