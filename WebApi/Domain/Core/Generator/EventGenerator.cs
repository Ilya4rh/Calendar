using Infrastructure.Entities;

namespace Core.Generator;

public class EventGenerator : IGenerator<EventEntity>
{
    public List<EventEntity> Generate(EventEntity activity)
    {
        return [activity];
    }
}