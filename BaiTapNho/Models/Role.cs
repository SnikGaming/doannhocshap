using System;
using System.Collections.Generic;

namespace BaiTapNho.models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Member> Members { get; } = new List<Member>();
}
