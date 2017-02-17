namespace TAIServer.Entities
{
    public class ProjectMember
    {
        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public long MemberId { get; set; }
        public Member Member { get; set; }
    }
}