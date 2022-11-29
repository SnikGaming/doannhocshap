using System;
using System.Collections.Generic;

namespace BaiTapNho.models;

public partial class Post
{
    public int Id { get; set; }

    public string? MemCreate { get; set; }

    public string? Title { get; set; }

    public string? FullContent { get; set; }

    public string? Img { get; set; }

    public int? Status { get; set; }

    public int? CatId { get; set; }

    public string? MemEdit { get; set; }

    public DateTime? TimeCreate { get; set; }

    public DateTime? TimeEdit { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual Member? MemCreateNavigation { get; set; }

    public virtual Member? MemEditNavigation { get; set; }
}
