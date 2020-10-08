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
            Assert.NotNull(result);
            // nuget FluentAssertions
            result.Should().BeEquivalentTo(tasktype);
         

        }
    }
}
