using Microsoft.AspNetCore.Mvc;
using TaskHandler.Models;
namespace TaskHandler.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly DBContext _context;

    public TaskController(ILogger<TaskController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetTasks")]
    public ActionResult<Task> GetAll()
    {
        var tasksList = _context.Tasks.ToList();
        return Ok(tasksList);
    }

    [HttpGet("{id}")]
    public ActionResult<Task> GetTask(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if(task != null)
            return Ok(task);
        else
            return StatusCode(StatusCodes.Status204NoContent, $"Cannot find a task with given id: {id}");
    }

    [HttpPost(Name = "AddTask")]
    public ActionResult<Task> AddTask(TaskDTO task)
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
        _context.Tasks.AddAsync(Task);
        _context.SaveChanges();
        return Ok(Task);
    }

    [HttpPost(Name = "AddListOfTasks")]
    public ActionResult<List<Task>> AddListOfTasks(List<TaskDTO> listOfTasks)
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
            _context.Tasks.AddAsync(Task);
            _context.SaveChangesAsync();
        }
        return Ok(listOfTasks);
    }

    [HttpPut("{id}")]
    public ActionResult<Task> EditTask(TaskDTO task, [FromRoute] Guid id)
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
        _context.SaveChangesAsync();
        return Ok(Task);
    }
    [HttpPut("{id}")]
    public ActionResult<Task> EditTaskStatus(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if(task != null)
        {
            task.Completed = !task.Completed;
            _context.SaveChangesAsync();
            return Ok(task);
        } 
        else
        {
            return StatusCode(StatusCodes.Status204NoContent, $"Cannot find a task to edit with given id: {id}");
        }
    }
    [HttpDelete("{id}")]
    public ActionResult<Task> DeleteTask(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null) 
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        } 
        else 
        {
            return StatusCode(StatusCodes.Status204NoContent, $"Cannot find a task to delete with given id: {id}");
        }
    }
 
}