using Common.Enums;

namespace DAL.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public Employee Author { get; set; }
        public string ExecutorId { get; set; }
        public Employee Executor { get; set; }
        public string Comment { get; set; }
        public JobStatus Status { get; set; }
        public int Priority { get; set; }
    }
}
