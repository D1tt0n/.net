﻿using D1tt0n.taskplanner.Domain.Models;

namespace D1tt0n.taskplanner.DataAccess.Abstractions2
{
    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem workItem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workItem);
        bool Remove(Guid id);
        void SaveChanges();
    }
}