using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskHandler.Models;
namespace TaskHandler.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly TaskDBContext _context;

    public TaskController(ILogger<TaskController> logger, TaskDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetTasks")]
    public List<Task> GetAll()
    {
        var tasksList = _context.Tasks.ToList();
        return tasksList;
    }

    [HttpGet("{id}")]
    public Task GetTask(Guid id)
    {
        return _context.Tasks.Find(id);
    }

    [HttpPost(Name = "AddTask")]
    public async Task<IActionResult> AddTask(TaskDTO task)
    {
        var Task = new Task()
        {
            Id = new Guid(),
            CreatedBy = task.CreatedBy,
            Description = task.Description,
            ExtendedInfo = task.ExtendedInfo,
            Location = task.Location,
            TaskName = task.TaskName,
            Completed = task.Completed,
            Date = DateTime.Now
        };
        await _context.Tasks.AddAsync(Task);
        await _context.SaveChangesAsync();
        return Ok(Task);
    }

    [HttpPost(Name = "AddListOfTasks")]
    public async Task<IActionResult> AddListOfTasks(List<TaskDTO> listOfTasks)
    {
        foreach(TaskDTO task in listOfTasks)
        {
            var Task = new Task()
            {
                Id = new Guid(),
                CreatedBy = task.CreatedBy,
                Description = task.Description,
                ExtendedInfo = task.ExtendedInfo,
                Location = task.Location,
                TaskName = task.TaskName,
                Completed = task.Completed,
                Date = DateTime.Now
            };
            await _context.Tasks.AddAsync(Task);
            await _context.SaveChangesAsync();
        }
        return Ok(listOfTasks);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditTask(TaskDTO task, [FromRoute] Guid id)
    {
        var Task = _context.Tasks.Find(id);
        if(Task != null)
        {
            Task.CreatedBy = task.CreatedBy;
            Task.Description = task.Description;
            Task.TaskName = task.TaskName;
            Task.ExtendedInfo = task.ExtendedInfo;
            Task.Location = task.Location;
            Task.Completed = task.Completed;
            Task.Date = DateTime.Now;
        }
        await _context.SaveChangesAsync();
        return Ok(Task);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTaskStatus(Guid id)
    {
        var Task = _context.Tasks.Find(id);
        if(Task != null)
        {
            Task.Completed = !Task.Completed;
        }
        await _context.SaveChangesAsync();
        return Ok(Task);
    }
    [HttpDelete("{id}")]
    public void DeleteTask(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null) 
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
 
}