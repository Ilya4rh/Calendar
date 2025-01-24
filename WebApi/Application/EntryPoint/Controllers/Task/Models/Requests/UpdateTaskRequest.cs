namespace WebApplication1.Controllers.Task.Models.Requests;

public record UpdateTaskRequest
{
    public required string Title { get; set; }
    public required Guid CreatorId { get; init; }
    
    public required Guid? RepeatId { get; init; }
    
    public required string? Description { get; set; }
}