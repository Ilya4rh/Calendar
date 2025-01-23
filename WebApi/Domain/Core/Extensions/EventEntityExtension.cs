using Core.Event.Models;
using Infrastructure.Entities;

namespace Core.Extensions;

public static class EventEntityExtension
{
    public static EventDto ConvertToEventDto(this EventEntity eventEntity, RepeatEntity? repeatEntity)
    {
        return new EventDto
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            CreatorId = eventEntity.CreatorId,
            StartDateTime = eventEntity.StartDateTime,
            EndDateTime = eventEntity.EndDateTime,
            Repeat = repeatEntity,
            Description = eventEntity.Description,
            GuestIds = eventEntity.GuestIds,
        };
    }
}