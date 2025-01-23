using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Core.Repeat;

public class RepeatService
{
    private readonly RepeatRepository repeatRepository;

    public RepeatService(RepeatRepository repeatRepository)
    {
        this.repeatRepository = repeatRepository;
    }

    public Guid AddRepeat(RepeatEntity repeat)
    {
        var newRepeat = repeatRepository.Save(repeat);
        return newRepeat.Id;
    }

    public RepeatEntity? GetRepeatById(Guid id)
    {
        return repeatRepository.GetById(id);
    }

    public RepeatEntity ChangeRepeat(RepeatEntity repeat)
    {
        return repeatRepository.Update(repeat);
    }
}