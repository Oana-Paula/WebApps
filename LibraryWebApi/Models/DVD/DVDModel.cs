using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.Models.DVD
{
    public class DVDModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Producer { get; set; }

        [Range(1960, 2022)]
        public int Year { get; set; }

        public string Description { get; set; }
    }
}