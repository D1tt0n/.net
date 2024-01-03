
using D1tt0n.taskplanner.DataAccess.Abstractions2;
using D1tt0n.taskplanner.Domain.Models;
using D1tt0n.taskplanner.Domain.Logic;
using Moq;

namespace D1tt0n.taskplanner.Domain.Logic.Tests1
{

    public class SimpleTaskPlannerTests
    {
        [Fact]
        public void CreatePlan_SortsTasksByPriorityDueDateAndTitle()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();
            var tasks = new List<WorkItem>
        {
            new WorkItem { Title = "Task1", Priority = Priority.Low, DueDate = DateTime.Now.AddDays(1) },
            new WorkItem { Title = "Task2", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(1) },
            new WorkItem { Title = "Task3", Priority = Priority.High, DueDate = DateTime.Now.AddDays(2) }
        };
            mockRepository.Setup(repo => repo.GetAll()).Returns(tasks.ToArray());

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var plannedTasks = taskPlanner.CreatePlan(tasks);

            // Assert
            Assert.Collection(
                plannedTasks,
                task => Assert.Equal("Task3", task.Title),
                task => Assert.Equal("Task2", task.Title),
                task => Assert.Equal("Task1", task.Title)
            );
        }

        [Fact]
        public void CreatePlan_ExcludesCompletedTasksFromPlan()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();
            var tasks = new List<WorkItem>
        {
            new WorkItem { Title = "Task1", IsCompleted = true },
            new WorkItem { Title = "Task2", IsCompleted = false },
            new WorkItem { Title = "Task3", IsCompleted = false }
        };
            mockRepository.Setup(repo => repo.GetAll()).Returns(tasks.ToArray());

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var plannedTasks = taskPlanner.CreatePlan(tasks);

            // Assert
            Assert.DoesNotContain(plannedTasks, task => task.IsCompleted);
        }
        [Fact]
        public void CreatePlan_IncludesAllRelevantUncompletedTasks()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();
            var tasks = new List<WorkItem>
    {
        new WorkItem { Title = "Task1", IsCompleted = true },
        new WorkItem { Title = "Task2", IsCompleted = false },
        new WorkItem { Title = "Task3", IsCompleted = false }
    };
            mockRepository.Setup(repo => repo.GetAll()).Returns(tasks.ToArray());

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var plannedTasks = taskPlanner.CreatePlan(tasks);

            // Assert
            Assert.Collection(
                plannedTasks,
                task => Assert.Equal("Task2", task.Title),
                task => Assert.Equal("Task3", task.Title)
            );
        }
    }
}