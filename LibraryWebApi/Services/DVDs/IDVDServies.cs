using LibraryWebApi.Models.DVD;
using System.Collections.Generic;

namespace LibraryWebApi.Services.DVDs
{
    public interface IDVDServies
    {
        public List<DVDModel> GetAllDVDs();

        DVDModel GetDVDById(int id);

        void CreateDVD(DVDModel dvd);

        void DeleteDVD(int id);

        void EditDVD(int id, DVDModel dvd);
    }
}