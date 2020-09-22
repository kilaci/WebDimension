using Microsoft.EntityFrameworkCore;

namespace webdimension.Data.Model
{
    public class WebdimensionDbContext : DbContext
    {
        public WebdimensionDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<TaskType> TaskTypes { get; set; }
    }
}