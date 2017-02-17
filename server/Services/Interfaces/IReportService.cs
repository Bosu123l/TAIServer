using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAIServer.Entities;

namespace TAIServer.Services.Interfaces
{
    public interface IReportService
    {
        int GetNumberOfTaskInProject(long projectId);

        int GetNumberOfTaskByStatus(long projectId, Status status);

        int GetNumberOfTaskFromMemberByStatus(long memberId, Status status);

    }
}
