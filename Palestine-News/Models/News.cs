using Palestine_News.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        //[Required]
        public int Userid { get; set; }

        [Required]
        public int CategoriesId { get; set; }

        // Navigation properties
        public virtual Category Categories { get; set; } = null!;
        public virtual User User { get; set; } = null!;



    }
}