using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class GroupStudent
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int TeacherId { get; set; }

    public byte Absent { get; set; }

    public byte Present { get; set; }

    public DateOnly? LearningDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;

    public virtual Account Teacher { get; set; } = null!;
}
