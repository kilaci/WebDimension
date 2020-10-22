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
    public class TaskTypeRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture fixture;

        public TaskTypeRepositoryTests(DatabaseFixture fixture)
        {
            this.fixture = fixture 
                ?? throw new ArgumentNullException(nameof(fixture));
            
        }


        /// <summary>
        /// Create teszt
        /// </summary>

        [Fact]
        public void TaskTypeRepositoryTests_AddedTaskTypesShouldBeAppearInRepository()
        {
            // Arrange - előkészületek

            // SUT: System Under Test
            var sut = new TaskTypeRepository(fixture.GetNewWebdimensionContext());
            var tasktype = new TaskType { Id = 1, Name = "Test Tasktype" };


            // Act - tesztelünk
            sut.Add(tasktype);
            var result = sut.GetById(tasktype.Id);


            // Assert - Kiértékelünk.
            result.Should().NotBeNull();
            // nuget FluentAssertions
            result.Should().BeEquivalentTo(tasktype);
        }


        /// <summary>
        /// Read test
        /// </summary>        
        [Fact]
        public void TaskTypeRepositoryTests_ExistingTaskTypesShouldBeAppearInRepository()
        {
            // Arrange - előkészületek
            // SUT: System Under Test
            var sut = new TaskTypeRepository(fixture.GetNewWebdimensionContext());
            var tasktype = new TaskType { Id = 1, Name = "Test Tasktype" };
            sut.Add(tasktype);
            var toUPdate = sut.GetById(tasktype.Id);

            // Act
            toUPdate.Name = "Modositott Tasktype";
            sut.Update(toUPdate);

            var afterupdate = sut.GetById(tasktype.Id);

            // Assert - Kiértékelünk.
            afterupdate.Should().BeEquivalentTo(toUPdate);
        }


        /// <summary>
        /// Update test
        /// </summary>        
        [Fact]
        public void TaskTypeRepositoryTests_ExistingTaskTypesShouldBeChange()
        {
            // Arrange - előkészületek
            var sut = new TaskTypeRepository(fixture.GetNewWebdimensionContext());
            var tasktype = new TaskType { Id = 1, Name = "Test Tasktype" };
            sut.Add(tasktype);
            
            // Act
            var toUPdate = sut.GetById(tasktype.Id);
            toUPdate.Name = "Modositott Tasktype";
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
            var sut = new TaskTypeRepository(fixture.GetNewWebdimensionContext());
            var tasktype = new TaskType { Id = 1, Name = "Test Tasktype" };
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
