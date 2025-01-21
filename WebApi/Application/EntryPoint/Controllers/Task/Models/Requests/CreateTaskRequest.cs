namespace WebApplication1.Controllers.Task.Models.Requests;

public record CreateTaskRequest
{
    public required string Title { get; set; }
    public required Guid CreatorId { get; init; }
    
    public required DateTime DateTime { get; init; }
    
    public required Guid? RepeatId { get; init; }
    
    public required string? Description { get; set; }
}