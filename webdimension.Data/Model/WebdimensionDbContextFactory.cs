using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace webdimension.Data.Model
{
    public class WebdimensionDbContextFactory : IDesignTimeDbContextFactory<WebdimensionDbContext>, IDisposable
    {
        private readonly string cn;
        private readonly SqliteConnection connection = null;

        public bool IsInMemoryDb()
        {
            var cb = new SqlConnectionStringBuilder(cn);    
            if (!cb.ContainsKey(GlobalStrings.DataSource))
            {
                throw new ArgumentException("hiányzik a Data Source","ConnectionString");
            }
            return GlobalStrings.SqlMemoryDb.Equals((string)cb[GlobalStrings.DataSource],
                                        StringComparison.OrdinalIgnoreCase);

        }

        public WebdimensionDbContextFactory()
        {
            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable(GlobalStrings.AspnetCoreEnvironment);
            var cbuilder = new ConfigurationBuilder()
                                .SetBasePath(basePath)    
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environment}.json", true)
                                .AddEnvironmentVariables()
                                ;
            var config = cbuilder.Build();
            cn = config.GetConnectionString(GlobalStrings.ConnectionName);
            if (IsInMemoryDb())
                {
                    connection = new SqliteConnection(cn);
                    connection.Open();
                }

            }

        public WebdimensionDbContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<WebdimensionDbContext>();
            
            if (IsInMemoryDb())
            {
                obuilder.UseSqlite(connection);
            }
            else
            {
                obuilder.UseSqlite(cn);
            }

            


            return new WebdimensionDbContext (obuilder.Options);
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
            
        }
    }
}

