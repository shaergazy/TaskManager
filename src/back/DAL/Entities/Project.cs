using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ContractorCompanyName { get; set; }
        public string? DirecrorId { get; set; }
        public Employee Director { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Priority {  get; set; }
        public List<ProjectEmployee> Employees { get; set; }
    }
}
