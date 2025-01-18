namespace Infrastructure.Entities;

public class TaskEntity : Entity
{
    public Guid CreatorId { get; init; }
    
    public DateTime DateTime { get; init; }
    
    public Guid? RepeatId { get; init; }
    
    public string? Description { get; init; }
}