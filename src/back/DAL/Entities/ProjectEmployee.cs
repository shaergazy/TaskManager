namespace DAL.Entities
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public string EmployeeId { get; set; }
        public Project Project { get; set; }
        public Employee Employee { get; set; }
    }
}
