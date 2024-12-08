using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [StringLength(100, ErrorMessage = "Genre cannot exceed 100 characters.")]
        public string Genre { get; set; }

        [Required]
      
        public string ISBN { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }
    }
}
