using System;
using webdimension.Data.Model;

namespace webdimension.Data.Repository
{
    public class TaskTypeRepository
    {
        private readonly WebdimensionDbContext db;

        public TaskTypeRepository()
        {
            //TODO: Antipattern
            var factory = new WebdimensionDbContextFactory();
            db = factory.CreateDbContext(new string[] {});
        }

        public TaskTypeRepository(WebdimensionDbContext db)
        {
            this.db = db 
                ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(TaskType tasktype)   
        {
            //TODO: Async
            db.TaskTypes.Add(tasktype);
        }


        public TaskType GetById(int Id)
        {
            //TODO: Async
            return db.TaskTypes.Find(Id);

        }

        public void Update(TaskType tasktype)
        {
            //TODO: visszatérési érték?
            db.TaskTypes.Update(tasktype);
            

        }

        public void Remove(TaskType tasktype)
        {
            //TODO: visszatérési érték?
            db.TaskTypes.Remove(tasktype);
        }
    }
}