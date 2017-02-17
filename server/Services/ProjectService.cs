using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TAI.Services;
using TAI.Utils;
using TAIServer.Entities;
using TAIServer.Entities.DataAccess;
using TAIServer.Models;
using TAIServer.Services.Interfaces;

namespace TAIServer.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(DataContext dataContext) : base(dataContext)
        {
        }

        public Project GetProject(long id)
        {
            var tempProjects = base.DataContext.Projects
                    .Include(x => x.TaskGroups).Include(x => x.ProjectManager).Include(x => x.ProjectMembers).ThenInclude(x => x.Member)
                    .SingleOrDefault(x => x.Id.Equals(id));
            if (tempProjects == null)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }
            return tempProjects;
        }

        public Project AddProject(ProjectModel project)
        {
            Project temProject;
            try
            {
                var dbManager = base.DataContext.Members.SingleOrDefault(m => m.Id == project.ProjectManagerId);
                var dbProject = new Project()
                {
                    Name = project.Name,
                    ProjectManager = dbManager
                };
                temProject = base.DataContext.Projects.Add(dbProject).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return temProject;
        }

        public Project UpdateProject(Project project)
        {
            Project temProject;
            try
            {
                var x = DataContext.Projects.Attach(project);

                var entry = DataContext.Entry(project);
                if(!String.IsNullOrEmpty(project.Name))
                    entry.Property(e => e.Name).IsModified = true;

                temProject = x.Entity;

                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return temProject;
        }

        public Project RemoveProject(long id)
        {
            Project temProject;
            try
            {
                temProject = base.DataContext.Projects.Remove(this.GetProject(id)).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }
            return temProject;
        }

        public IEnumerable<Project> GetProjects()
        {
            var tempProject = base.DataContext.Projects.ToList();
            if (tempProject == null || tempProject.Count == 0)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }
            return tempProject;
        }

        public Project AddMemberToProject(long memberId, long projectId)
        {
            var project = DataContext.Projects.SingleOrDefault(p => p.Id == projectId);
            var member = DataContext.Members.SingleOrDefault(m => m.Id == memberId);

            var projectMember = new ProjectMember()
            {
                Member = member,
                Project = project
            };

            project.ProjectMembers.Add(projectMember);
            DataContext.SaveChanges();

            return project;
        }

        public Project RemoveMemberFromProject(long memberId, long projectId)
        {
            var project = DataContext.Projects.SingleOrDefault(p => p.Id == projectId);
            var member = DataContext.Members.SingleOrDefault(m => m.Id == memberId);

            var projectMember = new ProjectMember()
            {
                Member = member,
                Project = project
            };

            project.ProjectMembers.Remove(projectMember);
            DataContext.SaveChanges();

            return project;
        }
    }
}