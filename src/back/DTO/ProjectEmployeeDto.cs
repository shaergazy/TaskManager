using System.ComponentModel.DataAnnotations;

namespace DTO
{
    /// <summary>
    /// Dto for ProjectEmployee
    /// </summary>
    public class ProjectEmployeeDto
    {
        /// <summary>
        /// Id of project
        /// </summary>
        [Required]
        public int ProjectId { get; set; }

        /// <summary>
        /// Id of employee
        /// </summary>
        [Required]
        public string EmployeeId { get; set; }
    }
}
