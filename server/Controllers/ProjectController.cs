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
    public class ProjectController
    {
        private readonly IProjectService _projects;

        public ProjectController(IProjectService projects)
        {
            _projects = projects;
        }

        [HttpGet("{id}")]
        public Project GetProject(long? id)
        {
            return _projects.GetProject(id.Value);
        }

        [HttpPost]
        public Project AddProject([FromBody]ProjectModel project)
        {
            return _projects.AddProject(project);
        }

        [HttpDelete("{id}")]
        public Project RemoveProject(long? id)
        {
            return _projects.RemoveProject(id.Value);
        }

        [HttpPut]
        public Project UpdateProject([FromBody]Project project)
        {
            return _projects.UpdateProject(project);
        }

        [HttpGet]
        public IEnumerable<Project> GetProjects()
        {
            return _projects.GetProjects();
        }

        [HttpPut("Member/Add")]
        public Project AddMemberToProject([FromBody]ProjectMemberModel model)
        {
            return _projects.AddMemberToProject(model.MemberId, model.ProjectId);
        }

        [HttpPut("Member/Remove")]
        public Project RemoveMemberFromProject([FromBody]ProjectMemberModel model)
        {
            return _projects.RemoveMemberFromProject(model.MemberId, model.ProjectId);
        }
    }
}