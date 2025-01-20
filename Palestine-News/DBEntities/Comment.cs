using System;
using System.Collections.Generic;

namespace Palestine_News.DBEntities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int Userid { get; set; }

    public int Newsid { get; set; }

    public string Text { get; set; } = null!;

    public virtual News News { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
