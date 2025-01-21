namespace WebApplication1.Controllers.Event.Models.Requests;

public record ChangeEventRequest
{
    public required Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public required Guid CreatorId { get; init; }
    
    public required DateTime StartDateTime { get; init; }
    
    public required DateTime EndDateTime { get; init; }
    
    public required int RepeatId { get; init; }
    
    public required string? Description { get; init; }
    
    public required Guid[]? GuestIds { get; init; }
}