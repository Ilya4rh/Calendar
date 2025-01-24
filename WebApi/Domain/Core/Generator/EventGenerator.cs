using Core.Event.Models;
using Core.Repeat.Models;
using Infrastructure.Enums;

namespace Core.Generator;

public class EventGenerator : IGenerator<EventDto>
{
    private const int DaysInWeek = 7;
    
    public List<EventDto> Generate(EventDto eventDto)
    {
        var repeat = eventDto.Repeat;
        
        if (repeat?.Interval is null or 0)
        {
            return new List<EventDto>();
        }

        var events = new List<EventDto>();
        var repeatEndDateTime = GetEndRepeatDateTime(repeat);

        var currentStartDateTime = eventDto.StartDateTime;
        var currentEndDateTime = eventDto.EndDateTime;
        
        while (currentStartDateTime < repeatEndDateTime)
        {
            var newStartDate = currentEndDateTime;
            var newEndDate = currentEndDateTime;
                
            switch (repeat.IntervalType)
            {
                case IntervalTypes.Day:
                    newStartDate = currentStartDateTime.AddDays(repeat.Interval.Value);
                    newEndDate = currentEndDateTime.AddDays(repeat.Interval.Value);
                    break;
                case IntervalTypes.Week:
                    newStartDate = currentStartDateTime.AddDays(repeat.Interval.Value * DaysInWeek);
                    newEndDate = currentEndDateTime.AddDays(repeat.Interval.Value * DaysInWeek);
                    break;
                case IntervalTypes.Month:
                    newStartDate = currentStartDateTime.AddMonths(repeat.Interval.Value);
                    newEndDate = currentEndDateTime.AddMonths(repeat.Interval.Value);
                    break;
                case IntervalTypes.Year:
                    newStartDate = currentStartDateTime.AddYears(repeat.Interval.Value);
                    newEndDate = currentEndDateTime.AddYears(repeat.Interval.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            currentStartDateTime = newStartDate;
            currentEndDateTime = newEndDate;

            var newEvent = eventDto with
            {
                StartDateTime = currentStartDateTime, 
                EndDateTime = currentEndDateTime, 
                Repeat = repeat
            };
            
            events.Add(newEvent);
        }
        
        return events;
    }
    
    private static DateTime GetEndRepeatDateTime(RepeatDto repeatDto)
    {
        if (repeatDto.DateEnd != null)
        {
            return repeatDto.DateEnd.Value;
        }

        return new DateTime(repeatDto.DateStart.Date.Year + 1, 1, 1);
    }
}