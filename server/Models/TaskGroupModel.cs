using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAIServer.Models
{
    public class TaskGroupModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public long ManagerId { get; set; }
    }
}
