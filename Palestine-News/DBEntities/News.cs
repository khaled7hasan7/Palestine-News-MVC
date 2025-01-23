using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Palestine_News.DBEntities;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Userid { get; set; }

    public int CategoriesId { get; set; }

    public virtual Category Categories { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;
}