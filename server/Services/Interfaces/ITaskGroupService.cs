using System.Collections.Generic;
using TAIServer.Entities;
using TAIServer.Models;

namespace TAIServer.Services.Interfaces
{
    public interface ITaskGroupService
    {
        TaskGroup GetTaskGroup(long id);

        TaskGroup AddTaskGroup(TaskGroupModel taskGroup);

        TaskGroup UpdateTaskGroup(TaskGroup taskGroup);

        TaskGroup RemoveTaskGroup(long id);

        IEnumerable<TaskGroup> TaskGroups();

        IEnumerable<Member> GetAvailableMembers(long id);
    }
}