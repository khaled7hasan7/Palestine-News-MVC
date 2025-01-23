using System.ComponentModel.DataAnnotations;

namespace Palestine_News.Models
{
    public class NewsViewModel
    {
      [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoriesId { get; set; }
        
    }
}