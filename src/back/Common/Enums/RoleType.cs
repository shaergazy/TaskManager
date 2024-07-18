using System.ComponentModel;

namespace Common.Enums;

public enum RoleType
{
    [Description("Admin")]
    Admin = 1,
    [Description("Director")]
    Director = 2,
    [Description("Manager")]
    Manager = 3,
    [Description("Employee")]
    Employee = 4,
}
