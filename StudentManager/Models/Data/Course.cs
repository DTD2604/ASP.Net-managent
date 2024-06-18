using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int DepartmentId { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
