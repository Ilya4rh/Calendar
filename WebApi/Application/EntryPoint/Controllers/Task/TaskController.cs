using Core.Task;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Task.Models.Requests;

namespace WebApplication1.Controllers.Task;

[Route("[controller]/[action]")]
[ApiController]
public class TaskController: ControllerBase
{
    private readonly TaskService taskService;

    public TaskController(TaskService taskService)
    {
        this.taskService = taskService;
    }

    [HttpPost]
    public ActionResult<bool> CreateTask([FromBody] CreateTaskRequest createTaskRequest)
    {
        taskService.AddTask(createTaskRequest.Title, createTaskRequest.CreatorId,
            createTaskRequest.DateTime, createTaskRequest.RepeatId, createTaskRequest.Description);
        return true;
    }

    [HttpGet]
    public ActionResult<TaskEntity> GetTaskByTitle(string title, Guid creatorId)
    {
        return taskService.GetTaskByTitle(title, creatorId);
    }

    [HttpPut]
    public ActionResult<bool> UpdateTask([FromBody] UpdateTaskRequest updateTaskRequest)
    {
        taskService.UpdateTask(updateTaskRequest.Title, updateTaskRequest.CreatorId, updateTaskRequest.Description, updateTaskRequest.RepeatId);
        return true;
    }
}