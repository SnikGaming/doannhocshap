using System;
using System.Collections.Generic;

namespace BaiTapNho.models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public int? Status { get; set; }

    public string? MemCreate { get; set; }

    public string? MemEdit { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? EditTime { get; set; }

    public virtual Member? MemCreateNavigation { get; set; }

    public virtual Member? MemEditNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}
