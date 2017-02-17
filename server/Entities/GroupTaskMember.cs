namespace TAIServer.Entities
{
    public class GroupTaskMember
    {
        public long GroupTaskId { get; set; }
        public TaskGroup TaskGroup { get; set; }
        public long MemberId { get; set; }
        public Member Member { get; set; }
    }
}
