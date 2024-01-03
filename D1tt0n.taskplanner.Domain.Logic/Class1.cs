
using D1tt0n.taskplanner.DataAccess.Abstractions2;
using D1tt0n.taskplanner.Domain.Models;

namespace D1tt0n.taskplanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private readonly IWorkItemsRepository _workItemsRepository;

        public SimpleTaskPlanner(IWorkItemsRepository workItemsRepository)
        {
            _workItemsRepository = workItemsRepository;
        }
        public List<WorkItem> CreatePlan(IEnumerable<WorkItem> tasks)
        {
            var pendingTasks = tasks.Where(task => !task.IsCompleted);

            var sortedTasks = pendingTasks
                .OrderByDescending(task => task.Priority)
                .ThenBy(task => task.DueDate)
                .ThenBy(task => task.Title)
                .ToList();
            return sortedTasks;
        }
    }
}


