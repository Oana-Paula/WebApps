using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.Database.Entities.DVD
{
    public class DVDEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Producer { get; set; }

        [Range(1980, 2023)]
        public int Year { get; set; }

        public string Description { get; set; }
    }
}