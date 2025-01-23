using Palestine_News.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Palestine_News.Models
{
    public class News
    {
        //  [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int NewsId { get; set; }

        // [Required]
        // [MaxLength(100)]
        // public required string Title { get; set; }

        // [Required]
        // [MaxLength(1500)]
        // public required string Content { get; set; }

        // // Foreign key for Category
        // public int CategoriesId { get; set; }

        // // Navigation property for Category
        // [ForeignKey("CategoriesId")]
        // public required Category Categories { get; set; }

        // // Foreign key for User
        // public int Userid { get; set; }

        // // Navigation property for User
        // [ForeignKey("Userid")]
        // public required User User { get; set; }

        // // Navigation property for Comments
        // public required ICollection<Comment> Comments { get; set; }

         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Content { get; set; }

        // Foreign key for Category
        public int CategoriesId { get; set; }

        // Navigation property for Category
        [ForeignKey("CategoriesId")]
        public Category Categories { get; set; }

        // Foreign key for User
        public int Userid { get; set; }

        // Navigation property for User
        [ForeignKey("Userid")]
        public User User { get; set; }

        // Navigation property for Comments
        public ICollection<Comment> Comments { get; set; }
    }
}