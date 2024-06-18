using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class Group
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int TermId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int StudentNumbers { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int TeacherId { get; set; }

    public int CaptainId { get; set; }

    public virtual Account Captain { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Account Teacher { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}
