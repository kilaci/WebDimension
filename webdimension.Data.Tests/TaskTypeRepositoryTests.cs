using System;
using Xunit;
using webdimension.Data.Model;
using webdimension.Data.Repository;
using FluentAssertions;


namespace webdimension.Data.Tests
{
    /// 
    /// CRUD és list tesztek
    /// 
    public class TaskTypeRepositoryTests
    {

        public TaskTypeRepositoryTests()
        {
            var factory = new WebdimensionDbContextFactory();
            var db = factory.CreateDbContext(new string[] { });

            db.Database.EnsureCreated();
        }

        
        /// <summary>
        /// Create teszt
        /// </summary>

        [Fact]
        public void TaskTypeRepositoryTests_AddedTaskTypesShouldBeAppearInRepository()
        {
            // Arrange - előkészületek
            
            // SUT: System Under Test
            var sut = new TaskTypeRepository();
            var tasktype = new TaskType { Id = 1, Name="Test Tasktype"};


            // Act - tesztelünk
            sut.Add(tasktype);
            var result = sut.GetById(tasktype.Id);


            // Assert - Kiértékelünk.
            result.Should().NotBeNull();
            // nuget FluentAssertions
            result.Should().BeEquivalentTo(tasktype);
        }


        /// <summary>
        /// Update test
        /// </summary>        
        [Fact]
        public void TaskTypeRepositoryTests_ExistingTaskTypesShouldBeAppearInRepository()
        {
            // Arrange - előkészületek
            // SUT: System Under Test
            var sut = new TaskTypeRepository();
            var tasktype = new TaskType { Id = 1, Name="Test Tasktype"};
            sut.Add(tasktype);
            var toUPdate = sut.GetById(tasktype.Id);

            // Act
            toUPdate.Name="Modositott Tasktype";
            sut.Update(toUPdate);

            var afterupdate = sut.GetById(tasktype.Id);

            // Assert - Kiértékelünk.
            afterupdate.Should().BeEquivalentTo(toUPdate);
             
        }


        /// <summary>
        /// Delete test
        /// </summary>        

        [Fact]
        public void TaskTypeRepositoryTests_ExistingTaskTypesShouldBeDelete()
        {
            // Arrange - előkészületek
            var sut = new TaskTypeRepository();
            var tasktype = new TaskType { Id = 1, Name="Test Tasktype"};
            sut.Add(tasktype);

            // Act
            var toDelete = sut.GetById(tasktype.Id);
            sut.Remove(toDelete);
            var afterDelete = sut.GetById(tasktype.Id);

            // Assert - Kiértékelünk.
            afterDelete.Should().BeNull();
        }

    }
}
