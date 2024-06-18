using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class ScheduleUser
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}
