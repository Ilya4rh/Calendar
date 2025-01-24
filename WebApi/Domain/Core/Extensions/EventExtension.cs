using Core.Event.Models;
using Core.Repeat.Models;
using Infrastructure.Entities;

namespace Core.Extensions;

public static class EventExtension
{
    public static EventDto ToDto(this EventEntity eventEntity, RepeatDto? repeatDto)
    {
        return new EventDto
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            StartDateTime = eventEntity.StartDateTime,
            EndDateTime = eventEntity.EndDateTime,
            Repeat = repeatDto
        };
    }

    public static EventEntity ToEntity(this EventDto eventDto, Guid? repeatId)
    {
        return new EventEntity
        {
            Id = eventDto.Id ?? Guid.NewGuid(),
            Title = eventDto.Title,
            StartDateTime = eventDto.StartDateTime,
            EndDateTime = eventDto.EndDateTime,
            RepeatId = repeatId
        };
    }
}