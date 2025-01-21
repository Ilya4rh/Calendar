
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Core.Task;

public class TaskService
{
    private readonly TaskRepository taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        this.taskRepository = taskRepository;
    }

    public void AddTask(string title ,Guid creatorId, DateTime dateTime, Guid? repeatId, string? description)
    {
        taskRepository.Save(new TaskEntity{Title = title, CreatorId = creatorId, DateTime = dateTime, RepeatId = repeatId,Description = description});
    }

    public TaskEntity GetTaskByTitle(string title, Guid creatorid)
    {
        return taskRepository.GetByTitle(title, creatorid);
    }

    public void UpdateTask(string title, Guid creatorId, string? description, Guid? repeatId)
    {
        GetTaskByTitle(title,creatorId).Description = description;
        GetTaskByTitle(title,creatorId).RepeatId = repeatId;
    }
}