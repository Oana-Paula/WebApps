using Library.Database;
using LibraryWebApi.Database.Entities.Magazines;
using LibraryWebApi.Models.Exceptions;
using System.Linq;

namespace LibraryWebApi.Services.Magazines.Searchers
{
    public class MagazineSearchServices : IMagazineSearchServices
    {
        private DatabaseContext _databaseContext;

        public MagazineSearchServices(DatabaseContext database)
        {
            _databaseContext = database;
        }

        public MagazineEntity SearchMagazineByID(int id, bool expected = false)
        {
            var magazine = _databaseContext.Magazines.FirstOrDefault(x => x.Id == id);
            if (magazine == null)
                throw new NotFoundException($"Magazine with id {id} not found.");
            return magazine;
        }

        public MagazineEntity GetMagazineByName(string name)
        {
            var magazine = _databaseContext.Magazines.FirstOrDefault(x => x.Name == name);
            return magazine;
        }
    }
}