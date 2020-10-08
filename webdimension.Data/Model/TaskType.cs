using System;

namespace webdimension.Data.Model
{
    public class TaskType : IEquatable<TaskType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TaskType);
        }

        public bool Equals (TaskType tasktype)
        {
            if (null==tasktype)
            {
                return false;
            }

            if  (Id!=tasktype.Id || Name!=tasktype.Name)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {    
                var hash = 27;
                hash = (13 * hash) + Id.GetHashCode();
                hash = (13 * hash) + Name.GetHashCode();
                return hash;
            }
        }

    }
}