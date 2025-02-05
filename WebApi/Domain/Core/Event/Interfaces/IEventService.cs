﻿using Core.Event.Models;

namespace Core.Event.Interfaces;

public interface IEventService
{
    List<EventDto> GetEventsForYear(int year);

    Guid CreateEvent(EventDto eventDto);

    EventDto ChangeEvent(EventDto eventDto);

    void DeleteEvent(Guid eventId);
}