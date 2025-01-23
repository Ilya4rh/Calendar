using Core.Repeat;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Repeat.Models.Requests;

namespace WebApplication1.Controllers.Repeat;

[Route("[controller]/[action]")]
[ApiController]
public class RepeatController: ControllerBase
{
    private readonly RepeatService repeatService;

    public RepeatController(RepeatService repeatService)
    {
        this.repeatService = repeatService;
    }
    
    [HttpGet]
    public ActionResult<RepeatEntity> GetRepeatById([FromQuery] Guid id)
    {
        var eventById = repeatService.GetRepeatById(id);
        
        return eventById;
    }

    [HttpPost]
    public ActionResult<Guid> AddRepeat([FromBody] CreateRepeatRequest request)
    {
        var repeat = new RepeatEntity
        {
            DateStart = request.DateStart,
            DateEnd = request.DateEnd,
            Days = request.Days,
            Interval = request.Interval,
            IntervalType = request.IntervalType,
        };

        var idNewRepeat = repeatService.AddRepeat(repeat);
        
        return idNewRepeat;
    }

    [HttpPut]
    public ActionResult<RepeatEntity> ChangeRepeat([FromBody] ChangeRepeatRequest request)
    {
        var repeat = new RepeatEntity
        {
            DateStart = request.DateStart,
            DateEnd = request.DateEnd,
            Days = request.Days,
            Interval = request.Interval,
            IntervalType = request.IntervalType,
        };

        var changedRepeat = repeatService.ChangeRepeat(repeat);
        
        return changedRepeat;
    }
}
