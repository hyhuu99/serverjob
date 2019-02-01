using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Models.Enum
{
    public enum JobType
    {
        [Description("Full Time")]
        FullTime = 1,

        [Description("Part Time")]
        PartTime,

        [Description("Temporary")]
        Temporary,

        [Description("Internship")]
        Internship
    }
}
