using Infrastructure.Enums;

namespace Core.Repeat.Models;

public record RepeatDto
{
    public Guid Id { get; init; }
    
    public required DateTime DateStart { get; init; }

    public required DateTime? DateEnd { get; init; }
    
    public required int? Interval { get; init; }
    
    public required IntervalTypes IntervalType { get; init; }
}