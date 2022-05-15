using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.Database.Entities.Magazines
{
    public class MagazineEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        public DateOnly Date { get; set; }
    }
}