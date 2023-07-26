using Microsoft.AspNetCore.Mvc;
using TaskHandler.Models;
namespace TaskHandler.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DBContext _context;
    public UserController(ILogger<UserController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet(Name = "GetUsers")]
    public ActionResult<Task> GetAll()
    {
        var tasksList = _context.Users.ToList();
        return Ok(tasksList);
    }
    [HttpPost(Name = "AddUser")]
    public ActionResult<Task> AddUser(UserDTO user)
    {
        var User = new User()
        {
            Id = new Guid(),
            RegisteredAt = DateTime.Now,
            UserName = user.UserName,
            Age = user.Age,
            Active = user.Active
        };
        _context.Users.AddAsync(User);
        _context.SaveChanges();
        return Ok(User);
    }
}