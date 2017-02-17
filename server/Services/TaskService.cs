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
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(DataContext dataContext) : base(dataContext)
        {
        }

        public Task AddTask(TaskModel task)
        {
            Task tempTask;

            try
            {
                var opTaskGroup = DataContext.TaskGroups.SingleOrDefault(p => p.Id == task.TaskGroupId);

                var opTask = new Task()
                {
                    TaskGroup = opTaskGroup,
                    Name = task.Name
                };

                tempTask = base.DataContext.Tasks.Add(opTask).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempTask;
        }

        public Task UpdateTask(TaskModel task)
        {
            Task tempTask;
            try
            {
                Member opMember = null;

                if (task.MemberId.HasValue)
                    opMember = DataContext.Members.SingleOrDefault(m => m.Id == task.MemberId);

                var opTask = new Task()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    ExpectedTaskDuration = task.ExpectedDurationTime.HasValue ? task.ExpectedDurationTime.Value : 0,
                    Duration = task.DurationTime.HasValue ? task.DurationTime.Value : 0,
                    Status = (Status)(task.Status.HasValue ? task.Status.Value : 0),
                    Member = opMember
                };
                var x = DataContext.Tasks.Attach(opTask);

                var entry = DataContext.Entry(opTask);
                if (!String.IsNullOrEmpty(task.Name))
                    entry.Property(e => e.Name).IsModified = true;
                if (!String.IsNullOrEmpty(task.Description))
                    entry.Property(e => e.Description).IsModified = true;
                if (task.ExpectedDurationTime.HasValue)
                    entry.Property(e => e.ExpectedTaskDuration).IsModified = true;
                if (task.DurationTime.HasValue)
                    entry.Property(e => e.Duration).IsModified = true;
                if (task.Status.HasValue)
                    entry.Property(e => e.Status).IsModified = true;
                if (task.MemberId.HasValue)
                    entry.Property(e => e.MemberId).IsModified = true;
                tempTask = x.Entity;

                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempTask;
        }

        public Task RemoveTask(long id)
        {
            Task tempTask = null;

            try
            {
                var task = this.GetTask(id);
                var dataContextTasks = base.DataContext.Tasks;
                if (dataContextTasks != null)
                {
                    tempTask = dataContextTasks.Remove(task).Entity;
                }

                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempTask;
        }

        public Task GetTask(long idTask)
        {
            return base.DataContext.Tasks.Include(x => x.Member).Single(x => x.Id == idTask);
        }

        public IEnumerable<Member> GetAvailableMembers(long id)
        {
            var members = DataContext.TaskGroups.Include(tg => tg.GroupTaskMembers).Include(tg => tg.TaskGroupManager).Include(tg => tg.Tasks).SingleOrDefault(tg => tg.Tasks.Where(t => t.Id == id).Count() > 0);
            if (members == null)
                throw new StatusCodeException((int)HttpStatusCode.BadRequest);

            var availableMembers = members.GroupTaskMembers.Select(gtm => gtm.Member).ToList();
            availableMembers.Add(members.TaskGroupManager);
            return availableMembers;
        }

        public IEnumerable<Task> GetTasks()
        {
            return base.DataContext.Tasks.ToList();
        }
    }
}