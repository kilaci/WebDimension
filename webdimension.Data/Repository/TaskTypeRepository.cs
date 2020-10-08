using System;
using webdimension.Data.Model;

namespace webdimension.Data.Repository
{
    public class TaskTypeRepository
    {
        private readonly WebdimensionDbContext db;

        public TaskTypeRepository()
        {
            var factory = new WebdemensionDbContextFactory();
            db = factory.CreateDbContext(new string[] {});
        }

       

        public void Add(TaskType entity)   
        {
            db.TaskTypes.Add(entity);

        }


        public TaskType GetById(int Id)
        {
            return db.TaskTypes.Find(Id);

        }

        public void Update(TaskType tasktype)
        {


        }

    }
}