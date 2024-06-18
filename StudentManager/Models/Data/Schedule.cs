using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class Schedule
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int GroupId { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int TeacherId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Repeats { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<ScheduleUser> ScheduleUsers { get; set; } = new List<ScheduleUser>();

    public virtual Account Teacher { get; set; } = null!;
}
