using System.ComponentModel.DataAnnotations;

namespace TAIServer.Entities
{
    public class Task
    {
        [Required]
        public long? Id { get; set; }

        public string Name { get; set; }
        public Status Status { get; set; }
        public int? PercentOfComplite { get; set; }
        public Member Member { get; set; }
        public long? MemberId { get; set; }

        public TaskGroup TaskGroup { get; set; }
        public long? TaskGroupId { get; set; }
        /// <summary>
        /// Expected Task Duration in hour
        /// </summary>
        public double ExpectedTaskDuration { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Duration in hours
        /// </summary>
        public double? Duration { get; set; }
    }
}