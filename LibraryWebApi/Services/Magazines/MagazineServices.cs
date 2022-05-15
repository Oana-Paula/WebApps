using Library.Database;
using LibraryWebApi.Database.Entities.Magazines;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Models.Magazines;
using LibraryWebApi.Services.Magazines.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Services.Magazines
{
    public class MagazineServices : IMagazineServices
    {
        private DatabaseContext _databaseContext;
        private IMagazineSearchServices _magazineSearchServices;

        public MagazineServices(DatabaseContext databaseContext, IMagazineSearchServices magazineSearchServices)
        {
            _databaseContext = databaseContext;
            _magazineSearchServices = magazineSearchServices;
        }

        #region Read

        public MagazineModel GetMagazineById(int id)
        {
            MagazineEntity magazineEntity = _magazineSearchServices.SearchMagazineByID(id, true);
            MagazineModel magazineModel = new()
            {
                Id = magazineEntity.Id,
                Name = magazineEntity.Name,
                Category = magazineEntity.Category,
                Date = magazineEntity.Date
            };

            return magazineModel;
        }

        public List<MagazineModel> GetAllMagazines()
        {
            List<MagazineModel> listMagazines = new List<MagazineModel>();
            var magazinesEntities = _databaseContext.Magazines;
            if (magazinesEntities.Count() == 0)
            {
                throw new NotFoundException("No magazine available");
            }
            listMagazines.AddRange(magazinesEntities.Select(value => new MagazineModel()
            {
                Id = value.Id,
                Name = value.Name,
                Category = value.Category,
                Date = value.Date
            }));
            return listMagazines;
        }

        #endregion Read

        public void CreateMagazine(MagazineModel magazineModel)
        {
            if (magazineModel.Id != null)
            {
                throw new BadRequestException("Id can not be added. Is primary key and will be added automatic on create");
            }
            MagazineEntity magazineExists = _magazineSearchServices.GetMagazineByName(magazineModel.Name);
            if (magazineExists != null)
            {
                throw new BadRequestException("Magazine is already in database");
            }
            MagazineEntity newMagazineEntity = new()
            {
                Name = magazineModel.Name,
                Category = magazineModel.Category,
                Date = magazineModel.Date
            };

            _databaseContext.Magazines.Add(newMagazineEntity);
            _databaseContext.SaveChanges();
        }

        public void EditMagazine(int id, MagazineModel magazineModel)
        {
            MagazineEntity magazineEntity = _magazineSearchServices.SearchMagazineByID(id, true);
            if (magazineModel.Id != null)
            {
                throw new BadRequestException("Id can not be edit");
            }
            if (magazineModel.Name != null)
                magazineEntity.Name = magazineModel.Name;
            if (magazineModel.Category != null)
                magazineEntity.Category = magazineModel.Category;
            if (magazineModel.Date > DateOnly.MinValue)
                magazineEntity.Date = magazineModel.Date;
            _databaseContext.Update(magazineEntity);
            _databaseContext.SaveChanges();
        }

        public void DeleteMagazine(int id)
        {
            MagazineEntity magazineEntity = _magazineSearchServices.SearchMagazineByID(id, true);
            _databaseContext.Magazines.Remove(magazineEntity);
            _databaseContext.SaveChanges();
        }
    }
}