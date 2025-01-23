using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class RepeatEntity : Entity
{
    public DateTime DateStart { get; init; }

    public DateTime? DateEnd { get; init; }
    
    public int? Interval { get; init; }
    
    public IntervalTypes IntervalType { get; init; }
}