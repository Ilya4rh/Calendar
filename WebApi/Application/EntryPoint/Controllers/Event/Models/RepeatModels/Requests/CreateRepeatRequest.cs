using Infrastructure.Enums;

namespace WebApplication1.Controllers.Event.Models.RepeatModels.Requests;

public record CreateRepeatRequest
{
    public required DateTime DateStart { get; init; }

    public required DateTime? DateEnd { get; init; }
    
    public required int? Interval { get; init; }
    
    public required IntervalTypes IntervalType { get; init; }
}