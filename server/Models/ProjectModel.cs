using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAIServer.Models
{
    public class ProjectModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectManagerId { get; set; }
    }
}
