using System;
using System.Collections.Generic;

namespace StudentManager.Data;

public partial class User
{
    public int Id { get; set; }

    public string ExtraCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public byte Gender { get; set; }

    public string? Avatar { get; set; }

    public string? Information { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
