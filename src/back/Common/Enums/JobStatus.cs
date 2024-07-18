using System.ComponentModel;

namespace Common.Enums
{
    public enum JobStatus
    {
        [Description("To do")]
        ToDo = 1,
        [Description("In progress")]
        InProgress = 2,
        [Description("Done")]
        Done = 3,      
    }
}
