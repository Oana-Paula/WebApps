using Library.Database;
using LibraryWebApi.Database.Entities.DVD;
using LibraryWebApi.Models.Exceptions;
using System.Linq;

namespace LibraryWebApi.Services.DVDs.Searchers
{
    public class DVDSearchServices : IDVDSearchServices
    {
        private DatabaseContext _databaseContext;

        public DVDSearchServices(DatabaseContext database)
        {
            _databaseContext = database;
        }

        public DVDEntity GetDVDByID(int id, bool expected = false)
        {
            var dvd = _databaseContext.DVDS.FirstOrDefault(x => x.Id == id);
            if (dvd == null)
                throw new NotFoundException($"DVD with id {id} not found.");
            return dvd;
        }

        public DVDEntity GetDVDByTitle(string title)
        {
            var dvd = _databaseContext.DVDS.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
            return dvd;
        }
    }
}