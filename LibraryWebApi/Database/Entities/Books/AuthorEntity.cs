using System.ComponentModel.DataAnnotations;

namespace Library.Database.Entities.Books
{
    public class AuthorEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}