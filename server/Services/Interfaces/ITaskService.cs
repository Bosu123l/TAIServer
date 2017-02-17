using System.Collections.Generic;
using TAIServer.Entities;
using TAIServer.Models;

namespace TAIServer.Services.Interfaces
{
    public interface ITaskService
    {
        Task AddTask(TaskModel task);

        Task UpdateTask(TaskModel task);

        Task RemoveTask(long id);

        Task GetTask(long idTask);

        IEnumerable<Task> GetTasks();

        IEnumerable<Member> GetAvailableMembers(long id);
    }
}