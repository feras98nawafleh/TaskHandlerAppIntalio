namespace TaskHandler;

public class User
{
    public Guid Id {set; get;}
    public DateTime RegisteredAt { get; set; }
    public required string UserName { get; set; }
    public required int Age { get; set; }
    public bool Active {get; set;} = false;
}
