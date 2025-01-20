using Palestine_News.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class News
    {
        [Key] // Marks this property as the primary key
        public int NewsId { get; set; }

        [Required] // Ensures the field is not null
        [StringLength(200)] // Limits the length of the title
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int Userid { get; set; }

        [Required]
        public int CategoriesId { get; set; }

        // Navigation properties for related entities
        public virtual Category Categories { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual User User { get; set; } = null!;
    }
}