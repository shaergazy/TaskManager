using System;
using System.Collections.Generic;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.Users;

public class User : IdentityUser, IIdHas<string>

{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public DateTime BirthdayDate { get; set; }
    public ICollection<UserRole> Roles { get; set; }
}
