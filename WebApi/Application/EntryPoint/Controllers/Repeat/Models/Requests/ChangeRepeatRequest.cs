using Infrastructure.Enums;

namespace WebApplication1.Controllers.Repeat.Models.Requests;

public record ChangeRepeatRequest
{
    public required DateTime DateStart { get; init; }

    public required DateTime? DateEnd { get; init; }

    public required  DayOfWeek[]? Days { get; init; }
    
    public required int? Interval { get; init; }
    
    public required IntervalTypes IntervalType { get; init; }
}