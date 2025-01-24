using WebApplication1.Controllers.Event.Models.RepeatModels;
using WebApplication1.Controllers.Event.Models.RepeatModels.Requests;

namespace WebApplication1.Controllers.Event.Models.EventModels.Requests;

public record ChangeEventRequest
{
    public required Guid Id { get; init; }
    
    public string? Title { get; init; }
    
    public required Guid CreatorId { get; init; }
    
    public required DateTime StartDateTime { get; init; }
    
    public required DateTime EndDateTime { get; init; }
    
    public required ChangeRepeatRequest? RepeatRequest { get; init; }
    
    public required Guid[]? GuestIds { get; init; }
}