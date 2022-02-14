using AppCore.Bussiness.Models.Results;
using Business.Models;
using Business.Services.Bases;
using DataAccess.Repositories.Bases;
using Entity.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Services
{
    public class DistrictService: IDistrictService
    {
        private readonly DistrictRepositoryBase _districtRepository;
        public DistrictService(DistrictRepositoryBase districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public IQueryable<DistrictModel> Query(Expression<Func<DistrictModel, bool>> predicate = null)
        {
            var query = _districtRepository.Query("Users").Select(d => new DistrictModel()
            {
                Id = d.Id,
                GuId = d.GuId,
                Name = d.Name,
                Users = d.Users.Select(u => new UserModel()
                {
                    Id = u.Id,
                    GuId = u.GuId,
                    Name = u.Name,
                    Surname = u.Surname
                }).ToList(),
                ProductionFacilities = d.ProductionFacilities.Select(p => new ProductionFacilityModel()
                {
                    Id = p.Id,
                    GuId = p.GuId,
                    Name = p.Name,
                    Location = p.Location,
                    ImagePath = p.ImagePath
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(DistrictModel model)
        {
            try
            {
                if (Query().Any(d => d.Name == model.Name.Trim() && d.CityId == model.CityId))
                    return new ErrorResult("Bir şehir altında aynı isimli iki ilçe bulunamaz!");
                var entity = new District()
                {
                    Name = model.Name.Trim(),
                    CityId = model.CityId
                };
                _districtRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(DistrictModel model)
        {
            try
            {
                if (Query().Any(d => d.Name == model.Name.Trim() && d.CityId == model.CityId && d.Id != model.Id))
                    return new ErrorResult("Bir şehir altında aynı isimli iki ilçe bulunamaz!");
                var entity = _districtRepository.Query(d => d.GuId == model.GuId).SingleOrDefault();
                entity.Name = model.Name.Trim();
                entity.CityId = model.CityId;
                _districtRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Delete(string guId)
        {
            try
            {
                if (Query(d => d.GuId == guId).SingleOrDefault().Users != null && Query(d => d.GuId == guId).SingleOrDefault().Users.Count > 0)
                    return new ErrorResult("Kayıtlı kullanıcılarımızın bulunduğu bir ilçe silinemez!");
                _districtRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _districtRepository?.Dispose();
        }
    }
}
