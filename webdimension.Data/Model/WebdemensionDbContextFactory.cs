using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace webdimension.Data.Model
{
    public class WebdemensionDbContextFactory : IDesignTimeDbContextFactory<WebdimensionDbContext>
    {
        public WebdimensionDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WebdimensionDbContext>();
            builder.UseSqlite("Data Source=webdimension.db;");
            return new WebdimensionDbContext (builder.Options);
        }
    }
}