using System;
using System.Collections.Generic;

namespace BaiTapNho.models;

public partial class Member
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Category> CategoryMemCreateNavigations { get; } = new List<Category>();

    public virtual ICollection<Category> CategoryMemEditNavigations { get; } = new List<Category>();

    public virtual ICollection<Post> PostMemCreateNavigations { get; } = new List<Post>();

    public virtual ICollection<Post> PostMemEditNavigations { get; } = new List<Post>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
