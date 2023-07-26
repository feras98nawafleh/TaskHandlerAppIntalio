using Microsoft.EntityFrameworkCore;

namespace TaskHandler
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

    }
}