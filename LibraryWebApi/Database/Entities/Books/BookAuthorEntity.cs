using System.ComponentModel.DataAnnotations;

namespace Library.Database.Entities.Books
{
    public class BookAuthorEntity
    {
        [Key]
        public int ID { get; set; }

        public BookEntity Book { get; set; }

        public AuthorEntity Author { get; set; }
    }
}