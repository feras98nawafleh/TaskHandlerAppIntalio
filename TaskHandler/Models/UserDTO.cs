namespace TaskHandler;

public class UserDTO
{
    public required string UserName { get; set; }
    public required int Age { get; set; }
    public bool Active {get; set;} = false;
}
