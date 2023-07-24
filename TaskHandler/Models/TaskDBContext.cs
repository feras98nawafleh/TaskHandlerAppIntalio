using Microsoft.EntityFrameworkCore;

namespace TaskHandler
{
    public class TaskDBContext : DbContext
    {
        public TaskDBContext(DbContextOptions<TaskDBContext> options)
            : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }

    }
}