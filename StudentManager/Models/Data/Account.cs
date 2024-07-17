using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class Account
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte Status { get; set; }

    public string? IpClient { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? LastLogout { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Group> GroupCaptains { get; set; } = new List<Group>();

    public virtual ICollection<GroupStudent> GroupStudentStudents { get; set; } = new List<GroupStudent>();

    public virtual ICollection<GroupStudent> GroupStudentTeachers { get; set; } = new List<GroupStudent>();

    public virtual ICollection<Group> GroupTeachers { get; set; } = new List<Group>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
    
}
