using System.Collections.Generic;
using TAIServer.Entities;
using TAIServer.Models;

namespace TAIServer.Services.Interfaces
{
    public interface IProjectService
    {
        Project GetProject(long id);

        Project AddProject(ProjectModel project);

        Project UpdateProject(Project project);

        Project RemoveProject(long id);

        IEnumerable<Project> GetProjects();

        Project AddMemberToProject(long memberId, long projectId);

        Project RemoveMemberFromProject(long memberId, long projectId);
    }
}