using System;
using System.Collections.Generic;

namespace Palestine_News.DBEntities;

public partial class Category
{
    public int CategoriesId { get; set; }

    public string CategoriesName { get; set; } = null!;

    public string Attribute { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
// sealed