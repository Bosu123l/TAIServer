using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TAIServer.Entities;
using TAIServer.Models;
using TAIServer.Services.Interfaces;

namespace TAIServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TaskGroupController
    {
        private readonly ITaskGroupService _taskGroupService;

        public TaskGroupController(ITaskGroupService taskGroupService)
        {
            _taskGroupService = taskGroupService;
        }

        [HttpGet("{id}")]
        public TaskGroup GetTaskGroup(long? id)
        {
            return _taskGroupService.GetTaskGroup(id.Value);
        }

        [HttpPost]
        public TaskGroup AddTaskGroup([FromBody]TaskGroupModel taskGroup)
        {
            return _taskGroupService.AddTaskGroup(taskGroup);
        }

        [HttpDelete("{id}")]
        public TaskGroup RemoveTaskGroup(long? id)
        {
            return _taskGroupService.RemoveTaskGroup(id.Value);
        }

        [HttpPut]
        public TaskGroup UpdateTaskGroup([FromBody]TaskGroup taskGroup)
        {
            return _taskGroupService.UpdateTaskGroup(taskGroup);
        }

        [HttpGet("{id}/Members")]
        public IEnumerable<Member> GetAvailableMembers(long id)
        {
            return _taskGroupService.GetAvailableMembers(id);
        }

        [HttpGet]
        public IEnumerable<TaskGroup> GetTaskGroups()
        {
            return _taskGroupService.TaskGroups();
        }
    }
}