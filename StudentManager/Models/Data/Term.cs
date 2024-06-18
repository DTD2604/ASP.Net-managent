using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class Term
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int Year { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
