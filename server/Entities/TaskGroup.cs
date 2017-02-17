using System.Collections.Generic;
using TAIServer.Controllers;

namespace TAIServer.Entities
{
    public class TaskGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public long? ProjectId { get; set; }
        public long TaskGroupManagerId { get; set; }
        public Member TaskGroupManager { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<GroupTaskMember> GroupTaskMembers { get; set; }
    }
}