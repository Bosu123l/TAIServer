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
    public class TaskGroupService : BaseService, ITaskGroupService
    {
        public TaskGroupService(DataContext dataContext) : base(dataContext)
        {
        }

        public TaskGroup GetTaskGroup(long id)
        {
            var tempTaskGrous = base.DataContext.TaskGroups.Include(x => x.TaskGroupManager).Include(x => x.Tasks)?
                    .ThenInclude(x => x.Member).Include(x => x.GroupTaskMembers).ThenInclude(x => x.Member).Single(x => x.Id.Equals(id));
            if (tempTaskGrous == null)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element");
            }

            return tempTaskGrous;
        }

        public TaskGroup AddTaskGroup(TaskGroupModel taskGroup)
        {
            TaskGroup tempTaskGroup;
            try
            {
                var opProject = DataContext.Projects.SingleOrDefault(p => p.Id == taskGroup.ProjectId);
                var opMember = DataContext.Members.SingleOrDefault(m => m.Id == taskGroup.ManagerId);
                var opTaskGroup = new TaskGroup()
                {
                    Name = taskGroup.Name,
                    Project = opProject,
                    TaskGroupManager = opMember
                };
                tempTaskGroup = base.DataContext.TaskGroups.Add(opTaskGroup).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempTaskGroup;
        }

        public TaskGroup UpdateTaskGroup(TaskGroup taskGroup)
        {
            TaskGroup tempTaskGroup;
            try
            {
                var x = DataContext.TaskGroups.Attach(taskGroup);

                var entry = DataContext.Entry(taskGroup);

                if(!String.IsNullOrEmpty(taskGroup.Name))
                    entry.Property(e => e.Name).IsModified = true;

                tempTaskGroup = x.Entity;

                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempTaskGroup;
        }

        public TaskGroup RemoveTaskGroup(long id)
        {
            TaskGroup tempTaskGroup;
            try
            {
                var taskGroup = this.GetTaskGroup(id);

                tempTaskGroup = base.DataContext.TaskGroups.Remove(taskGroup).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element");
            }
            return tempTaskGroup;
        }

        public IEnumerable<TaskGroup> TaskGroups()
        {
            var taskGroups = base.DataContext.TaskGroups.ToList();
            if (taskGroups == null || taskGroups.Count == 0)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element");
            }
            return taskGroups;
        }

        public IEnumerable<Member> GetAvailableMembers(long id)
        {
            var project = DataContext.Projects.Include(p => p.ProjectManager).Include(p => p.ProjectMembers).Include(p => p.TaskGroups).SingleOrDefault(p => p.TaskGroups.Where(t => t.Id == id).Count() > 0);
            if (project == null)
                throw new StatusCodeException((int)HttpStatusCode.BadRequest);

            var availableMembers = project.ProjectMembers.Select(gtm => gtm.Member).ToList();
            availableMembers.Add(project.ProjectManager);
            return availableMembers;
        }
    }
}