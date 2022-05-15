using System.Collections.Generic;

namespace LibraryWebApi.Models.Books
{
    public class BookModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string Category { get; set; }
        public string Description { get; set; }

        private List<AuthorModel> authorList;

        public BookModel()
        {
            AuthorList = new List<AuthorModel>();
        }

        public List<AuthorModel> AuthorList
        {
            get { return authorList; }
            set { authorList = value; }
        }
    }
}