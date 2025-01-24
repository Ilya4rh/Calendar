namespace Infrastructure.Entities;

public class EventEntity : UserScopeEntity
{
    public string Title { get; init; }
    
    public DateTime StartDateTime { get; init; }
    
    public DateTime EndDateTime { get; init; }
    
    public Guid? RepeatId { get; init; }
    
    public Guid[]? GuestIds { get; init; }
}