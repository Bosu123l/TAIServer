using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAIServer.Entities;
using TAIServer.Services.Interfaces;

namespace TAIServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("GetNumberOfTaskInProject/{projectId}")]
        [HttpGet]
        public int GetNumberOfTaskInProject(long projectId)
        {
            return _reportService.GetNumberOfTaskInProject(projectId);
        }

        [Route("GetNumberOfTaskByStatus/{projectId}/{status}")]
        [HttpGet]
        public int GetNumberOfTaskByStatus(long projectId, int status)
        {
            return _reportService.GetNumberOfTaskByStatus(projectId, (Status)status);
        }

        [Route("GetNumberOfTaskFromMemberByStatus/{memberId}/{status}")]
        [HttpGet]
        public int GetNumberOfTaskFromMemberByStatus(long memberId, int status)
        {
            return _reportService.GetNumberOfTaskFromMemberByStatus(memberId, (Status)status);
        }
    }
}
