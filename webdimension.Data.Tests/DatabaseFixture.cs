using System;
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
            db.Database.EnsureCreated();
        }

        public WebdimensionDbContext GetNewWebdimensionContext()
        {
            return factory.CreateDbContext(new string[] {}); 
        }

        public void Dispose()
        {
                var db = GetNewWebdimensionContext();
                db.Database.EnsureDeleted();
                db.Dispose();
            
        }
    }
}