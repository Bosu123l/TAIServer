using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TAIServer.Controllers;

namespace TAIServer.Entities
{
    public class Member
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

        public IEnumerable<ProjectMember> ProjectsMembers { get; set; }
        public IEnumerable<GroupTaskMember> GroupTaskMembers { get; set; }
        public long? TaskId { get; set; }
        public IEnumerable<Project> ManagedProjects { get; set; }
        public long? ManagedProjectId { get; set; }

        public IEnumerable<TaskGroup> ManagedTaskGroups { get; set; }
        public long? ManagedTaskGroupId { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.Surname}";
        }
    }
}