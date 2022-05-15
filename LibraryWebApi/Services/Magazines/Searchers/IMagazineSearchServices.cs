using LibraryWebApi.Database.Entities.Magazines;

namespace LibraryWebApi.Services.Magazines.Searchers
{
    public interface IMagazineSearchServices
    {
        MagazineEntity SearchMagazineByID(int id, bool expected = false);

        MagazineEntity GetMagazineByName(string name);
    }
}