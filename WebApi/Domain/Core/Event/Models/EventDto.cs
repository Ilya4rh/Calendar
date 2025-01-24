using Core.Repeat.Models;

namespace Core.Event.Models;

public record EventDto
{
    public Guid? Id { get; init; }
    
    public required string Title { get; init; }
    
    public required DateTime StartDateTime { get; init; }
    
    public required DateTime EndDateTime { get; init; }
    
    public required RepeatDto? Repeat { get; init; }
}