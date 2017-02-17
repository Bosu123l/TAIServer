using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAIServer.Models
{
    public class TaskModel
    {
        public long Id { get; set; }
        public long? MemberId { get; set; }
        public int? ExpectedDurationTime { get; set; }
        public int? DurationTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public long TaskGroupId { get; set; }
    }
}
