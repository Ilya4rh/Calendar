using Core.Event.Models;
using Core.Repeat.Models;
using Infrastructure.Enums;

namespace Core.Generator;

public class EventGenerator : IGenerator<EventDto>
{
    private const int DaysInWeek = 7;
    
    public List<EventDto> Generate(EventDto eventDto)
    {
        var events = new List<EventDto>();
        var repeat = eventDto.Repeat;
        
        if (repeat?.Interval is null or 0)
        {
            return events;
        }

        var repeatEndDateTime = GetEndRepeatDateTime(repeat);

        var currentStartDateTime = eventDto.StartDateTime;
        var currentEndDateTime = eventDto.EndDateTime;
        
        while (currentStartDateTime < repeatEndDateTime)
        {
            currentStartDateTime = GetNextDate(currentStartDateTime, repeat.Interval.Value, repeat.IntervalType);
            currentEndDateTime = GetNextDate(currentEndDateTime, repeat.Interval.Value, repeat.IntervalType);

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

    private static DateTime GetNextDate(DateTime currentDate, int interval, IntervalTypes intervalType)
    {
        return intervalType switch
        {
            IntervalTypes.Day => currentDate.AddDays(interval),
            IntervalTypes.Week => currentDate.AddDays(interval * DaysInWeek),
            IntervalTypes.Month => currentDate.AddMonths(interval),
            IntervalTypes.Year => currentDate.AddYears(interval),
            _ => throw new ArgumentOutOfRangeException()
        };
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