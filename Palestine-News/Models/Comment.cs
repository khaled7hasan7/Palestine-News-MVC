using Palestine_News.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class Comment
    {
        [Key] // Marks this property as the primary key
        public int CommentId { get; set; }

        [Required] // Ensures the field is not null
        public int Userid { get; set; }

        [Required]
        public int Newsid { get; set; }

        [Required]
        [StringLength(500)] // Limits the length of the comment text
        public string Text { get; set; } = null!;

        // Navigation properties for related entities
        public virtual News News { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}