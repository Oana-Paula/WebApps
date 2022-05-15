using LibraryWebApi.Database.Entities.DVD;

namespace LibraryWebApi.Services.DVDs.Searchers
{
    public interface IDVDSearchServices
    {
        DVDEntity GetDVDByID(int id, bool expected = false);

        DVDEntity GetDVDByTitle(string title);
    }
}