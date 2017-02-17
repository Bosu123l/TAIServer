using System.Collections.Generic;

namespace TAIServer.Entities
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaskGroup> TaskGroups { get; set; }
        public Member ProjectManager { get; set; }
        public long? ProjectManagerId { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}