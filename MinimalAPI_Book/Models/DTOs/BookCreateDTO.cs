using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_Book.Models.DTOs
{
    public class BookCreateDTO
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsAvalible { get; set; }
    }
}
