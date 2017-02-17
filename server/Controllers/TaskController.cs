using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TAIServer.Entities;
using TAIServer.Services.Interfaces;
using TAIServer.Models;

namespace TAIServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TaskController
    {
        private readonly ITaskService _tasks;

        public TaskController(ITaskService tasks)
        {
            _tasks = tasks;
        }

        [HttpGet("{id}")]
        public Task GetTask(long? id, string name)
        {
            return _tasks.GetTask(id.Value);
        }

        [HttpGet("{id}/Members")]
        public IEnumerable<Member> GetAvailableMembers(long id)
        {
            return _tasks.GetAvailableMembers(id);
        }

        [HttpPost]
        public Task AddTask([FromBody]TaskModel task)
        {
            return _tasks.AddTask(task);
        }

        [HttpDelete("{id}")]
        public Task RemoveTask(long? id)
        {
            return _tasks.RemoveTask(id.Value);
        }

        [HttpPut]
        public Task UpdateTask([FromBody]TaskModel task)
        {
            return _tasks.UpdateTask(task);
        }

        [HttpGet]
        public IEnumerable<Task> GetTasks()
        {
            return _tasks.GetTasks();
        }
    }
}