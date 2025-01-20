using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class Category
    {
        [Key] // Marks this property as the primary key
        public int CategoriesId { get; set; }

        [Required] // Ensures the field is not null
        [StringLength(100)] // Limits the length of the category name
        public string CategoriesName { get; set; } = null!;

        [Required]
        [StringLength(100)] // Limits the length of the attribute
        public string Attribute { get; set; } = null!;

        // Navigation property for related news articles
        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}