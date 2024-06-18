using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManager.Data;

public partial class Department
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public DateOnly? DateBeginning { get; set; }

    public byte Status { get; set; }

    public string? Logo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int LeaderId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [BindNever]
    [ForeignKey("LeaderId")]
    public virtual Account Leader { get; set; } = null!;
}
