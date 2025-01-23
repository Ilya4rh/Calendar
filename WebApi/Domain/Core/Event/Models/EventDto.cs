using Infrastructure.Entities;

namespace Core.Event.Models;

public record EventDto
{
    public required Guid Id { get; init; }
    
    public required string? Title { get; init; }
    
    public required Guid CreatorId { get; init; }
    
    public required DateTime StartDateTime { get; init; }
    
    public required DateTime EndDateTime { get; init; }
    
    public required RepeatEntity? Repeat { get; init; }
    
    public required string? Description { get; init; }
    
    public required Guid[]? GuestIds { get; init; }
}