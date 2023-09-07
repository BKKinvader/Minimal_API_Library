namespace MinimalAPI_Book.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string  Description { get; set; }
        public bool IsAvalible { get; set; }

        public DateTime? Created { get; set; }     
        public DateTime? DateOfBorrowed { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
