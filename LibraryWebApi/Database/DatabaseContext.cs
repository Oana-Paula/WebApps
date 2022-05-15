using Library.Database.Entities.Books;
using LibraryWebApi.Database.Entities.DVD;
using LibraryWebApi.Database.Entities.Magazines;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        public DbSet<BookAuthorEntity> BookAuthorMap { get; set; }

        public DbSet<DVDEntity> DVDS { get; set; }
        public DbSet<MagazineEntity> Magazines { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}