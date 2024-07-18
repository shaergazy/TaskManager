using DAL.Entities.Users;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Employee : User
    {
        public DateTime BirthDate { get; set; }
        public List<ProjectEmployee> Projects { get; set; }
    }
}
