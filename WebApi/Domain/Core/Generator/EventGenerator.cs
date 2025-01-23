using Core.Event.Models;
using Core.Repeat.Models;
using Infrastructure.Entities;
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
            return [eventDto];
        }

        var events = new List<EventDto>();
        var repeatEndDateTime = GetEndRepeatDateTime(repeat);

        var currentStartDateTime = eventDto.StartDateTime;
        var currentEndDateTime = eventDto.EndDateTime;
        
        while (currentStartDateTime < repeatEndDateTime)
        {
            var newDate = currentEndDateTime;
            var newDate2 = currentEndDateTime;
                
            switch (repeat.IntervalType)
            {
                case IntervalTypes.Day:
                    newDate = currentStartDateTime.AddDays(repeat.Interval.Value);
                    newDate2 = currentEndDateTime.AddDays(repeat.Interval.Value);
                    break;
                case IntervalTypes.Week:
                    newDate = currentStartDateTime.AddDays(repeat.Interval.Value * DaysInWeek);
                    newDate2 = currentEndDateTime.AddDays(repeat.Interval.Value * DaysInWeek);
                    break;
                case IntervalTypes.Month:
                    newDate = currentStartDateTime.AddMonths(repeat.Interval.Value);
                    newDate2 = currentEndDateTime.AddMonths(repeat.Interval.Value);
                    break;
                case IntervalTypes.Year:
                    newDate = currentStartDateTime.AddYears(repeat.Interval.Value);
                    newDate2 = currentEndDateTime.AddYears(repeat.Interval.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            currentStartDateTime = newDate;
            currentEndDateTime = newDate2;

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