using LibraryWebApi.Models.Magazines;
using System.Collections.Generic;

namespace LibraryWebApi.Services.Magazines
{
    public interface IMagazineServices
    {
        public List<MagazineModel> GetAllMagazines();

        MagazineModel GetMagazineById(int id);

        void CreateMagazine(MagazineModel magazinModel);

        void DeleteMagazine(int id);

        void EditMagazine(int id, MagazineModel magazinModel);
    }
}