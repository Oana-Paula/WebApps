using LibraryWebApi.Services.ConverterServices;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LibraryWebApi.Models.Magazines
{
    public class MagazineModel
    {
        [ReadOnly(true)]
        public int? Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        [property: JsonConverter(typeof(DateOnlyConverterService))]
        public DateOnly Date { get; set; }
    }
}