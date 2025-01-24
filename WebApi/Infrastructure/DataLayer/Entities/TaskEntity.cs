namespace Infrastructure.Entities;

public class TaskEntity : Entity
{
    public string Title { get; set; }
    public Guid CreatorId { get; init; }
    
    public DateTime DateTime { get; init; }
    
    public Guid? RepeatId { get; set; }
    
    public string? Description { get; set; }
}