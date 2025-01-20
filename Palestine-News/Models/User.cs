 

using global::Palestine_News.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class User
    {
        [Key] // Marks this property as the primary key
        public int UserId { get; set; }

        [Required] // Ensures the field is not null
        [StringLength(100)] // Limits the length of the string
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress] // Validates that the input is a valid email address
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)] // Specifies that this field is a password
        public string Password { get; set; } = null!;

        // Navigation properties for related entities
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}