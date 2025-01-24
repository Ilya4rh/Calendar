using Infrastructure.Enums;

namespace WebApi.Controllers.Event.Models.RepeatModels.Requests;

public record ChangeRepeatRequest
{
    public required Guid? Id { get; init; }
    
    public required DateTime DateStart { get; init; }

    public required DateTime? DateEnd { get; init; }
    
    public required int? Interval { get; init; }
    
    public required IntervalTypes IntervalType { get; init; }
}