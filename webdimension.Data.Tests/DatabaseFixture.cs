using System;
using Microsoft.EntityFrameworkCore;
using webdimension.Data.Model;


namespace webdimension.Data.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly WebdimensionDbContextFactory factory;

        public DatabaseFixture()
        {
            factory = new WebdimensionDbContextFactory();
            var db = GetNewWebdimensionContext();

            if (factory.IsInMemoryDb())
            {
                // Memória db
                db.Database.EnsureCreated();
            }
            else
            {
                // csak file DBban
                db.Database.Migrate();
            }   
        }

        public WebdimensionDbContext GetNewWebdimensionContext()
        {
            return factory.CreateDbContext(new string[] {}); 
        }

        public void Dispose()
        {
                var db = GetNewWebdimensionContext();
                factory.Dispose();
                db.Database.EnsureDeleted();
                db.Dispose();
            
        }
    }
}