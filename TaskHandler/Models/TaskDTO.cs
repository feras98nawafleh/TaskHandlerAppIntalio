namespace TaskHandler.Models;

public class TaskDTO 
{
    public required string TaskName { get; set; }
    public required string Description { get; set; }
    public required string ExtendedInfo { get; set; }
    public required string Location {set; get;}
    public required string CreatedBy {get; set; }
    public bool Completed {get; set;} = false;
}