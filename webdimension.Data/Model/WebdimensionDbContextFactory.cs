using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace webdimension.Data.Model
{
    public class WebdimensionDbContextFactory : IDesignTimeDbContextFactory<WebdimensionDbContext>
    {
        public WebdimensionDbContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<WebdimensionDbContext>();
            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable(GlobalStrings.AspnetCoreEnvironment);
            var cbuilder = new ConfigurationBuilder()
                                .SetBasePath(basePath)    
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environment}.json", true)
                                .AddEnvironmentVariables()
                                ;
            var config = cbuilder.Build();
            var cn = config.GetConnectionString(GlobalStrings.ConnectionName);

            //TODO: beállítás from appsetting
            obuilder.UseSqlite(cn);
            return new WebdimensionDbContext (obuilder.Options);
        }
    }
}

