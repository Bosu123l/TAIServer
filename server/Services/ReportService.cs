using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TAI.Services;
using TAI.Utils;
using TAIServer.Entities;
using TAIServer.Entities.DataAccess;
using TAIServer.Services.Interfaces;

namespace TAIServer.Services
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(DataContext dataContext) : base(dataContext)
        {

        }

        public int GetNumberOfTaskInProject(long projectId)
        {
            try
            {
                var project = DataContext.Projects.Include(x=>x.TaskGroups).ThenInclude(x=>x.Tasks).FirstOrDefault(q=>q.Id.Equals(projectId));
                if (project != null)
                    return project.TaskGroups.Sum(q => q.Tasks.Count());

                throw new StatusCodeException((int)HttpStatusCode.NotAcceptable);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int GetNumberOfTaskByStatus(long projectId, Status status)
        {
            try
            {
                var project = DataContext.Projects.Include(x => x.TaskGroups).ThenInclude(x => x.Tasks).FirstOrDefault(q => q.Id.Equals(projectId));
                if (project != null)
                    return project.TaskGroups.Sum(q => q.Tasks.Count(t=>t.Status.Equals(status)));
                throw new StatusCodeException((int)HttpStatusCode.NotAcceptable);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int GetNumberOfTaskFromMemberByStatus(long memberId, Status status)
        {
            try
            {
                var member = DataContext.Members.Include(x=>x.Tasks).FirstOrDefault(q => q.Id.Equals(memberId));
                if (member != null)
                    return member.Tasks.Count(t=>t.Status.Equals(status));
                throw new StatusCodeException((int)HttpStatusCode.NotAcceptable);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


    }
}
