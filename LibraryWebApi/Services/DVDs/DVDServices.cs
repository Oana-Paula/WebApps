using Library.Database;
using LibraryWebApi.Database.Entities.DVD;
using LibraryWebApi.Models.DVD;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.DVDs.Searchers;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Services.DVDs
{
    public class DVDServices : IDVDServies
    {
        private DatabaseContext _databaseContext;
        private IDVDSearchServices _dvdSearchServices;

        public DVDServices(DatabaseContext databaseContext, IDVDSearchServices dVDSearchServices)
        {
            _databaseContext = databaseContext;
            _dvdSearchServices = dVDSearchServices;
        }

        public DVDModel GetDVDById(int id)
        {
            DVDEntity dvdEntity = _dvdSearchServices.GetDVDByID(id, true);
            DVDModel dvdModel = new()
            {
                Id = dvdEntity.Id,
                Title = dvdEntity.Title,
                Year = dvdEntity.Year,
                Producer = dvdEntity.Producer,
                Description = dvdEntity.Description
            };

            return dvdModel;
        }

        public List<DVDModel> GetAllDVDs()
        {
            List<DVDModel> listDVD = new List<DVDModel>();
            var dvdEntities = _databaseContext.DVDS;
            if (dvdEntities.Count() == 0)
            {
                throw new NotFoundException("No magazine available");
            }
            listDVD.AddRange(dvdEntities.Select(value => new DVDModel()
            {
                Id = value.Id,
                Title = value.Title,
                Year = value.Year,
                Producer = value.Producer,
                Description = value.Description
            }));
            return listDVD;
        }

        public void CreateDVD(DVDModel dvdModel)
        {
            if (dvdModel.Id != null)
            {
                throw new BadRequestException("Id can not be added. Is primary key and will be added automatic on create");
            }
            DVDEntity dvdExists = _dvdSearchServices.GetDVDByTitle(dvdModel.Title);
            if (dvdExists != null)
            {
                throw new BadRequestException("DVD is already in database");
            }
            DVDEntity newDVDEntity = new()
            {
                Year = dvdModel.Year,
                Title = dvdModel.Title,
                Producer = dvdModel.Producer,
                Description = dvdModel.Description
            };
            _databaseContext.DVDS.Add(newDVDEntity);
            _databaseContext.SaveChanges();
        }

        public void EditDVD(int id, DVDModel dvdModel)
        {
            if (dvdModel.Id != null)
            {
                throw new BadRequestException("Id can not be edit");
            }
            DVDEntity dvdEntity = _dvdSearchServices.GetDVDByID(id, true);
            if (dvdModel.Title != null)
                dvdEntity.Title = dvdModel.Title;
            if (dvdModel.Producer != null)
                dvdEntity.Producer = dvdModel.Producer;
            if (dvdModel.Year != 0)
                dvdEntity.Year = dvdModel.Year;
            if (dvdModel.Description != null)
                dvdEntity.Description = dvdModel.Description;
            _databaseContext.Update(dvdEntity);
            _databaseContext.SaveChanges();
        }

        public void DeleteDVD(int id)
        {
            DVDEntity dvdEntity = _dvdSearchServices.GetDVDByID(id, true);
            _databaseContext.DVDS.Remove(dvdEntity);
            _databaseContext.SaveChanges();
        }
    }
}