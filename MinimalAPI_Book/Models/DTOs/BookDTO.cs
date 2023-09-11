namespace MinimalAPI_Book.Models.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public bool IsAvalible { get; set; }
        public DateTime? Created { get; set; }
    }
}
