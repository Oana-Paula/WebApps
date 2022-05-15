using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Database.Entities.Books
{
    public class BookEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public List<BookAuthorEntity> BookAuthor { get; set; }
    }
}