using Core.Repeat.Models;
using Infrastructure.Entities;

namespace Core.Extensions;

public static class RepeatExtension
{
    public static RepeatDto ToDto(this RepeatEntity repeatEntity)
    {
        return new RepeatDto
        {
            Id = repeatEntity.Id,
            DateStart = repeatEntity.DateStart,
            DateEnd = repeatEntity.DateEnd,
            Interval = repeatEntity.Interval,
            IntervalType = repeatEntity.IntervalType
        };
    }

    public static RepeatEntity ToEntity(this RepeatDto repeatDto)
    {
        return new RepeatEntity
        {
            Id = repeatDto.Id ?? Guid.NewGuid(),
            DateStart = repeatDto.DateStart,
            DateEnd = repeatDto.DateEnd,
            Interval = repeatDto.Interval,
            IntervalType = repeatDto.IntervalType
        };
    }
}