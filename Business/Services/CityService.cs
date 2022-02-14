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
    public class CityService : ICityService
    {
        private readonly CityRepositoryBase _cityRepository;
        public CityService(CityRepositoryBase cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IQueryable<CityModel> Query(Expression<Func<CityModel, bool>> predicate = null)
        {
            var query = _cityRepository.Query("Districts", "Users").Select(c => new CityModel()
            {
                Id = c.Id,
                GuId = c.GuId,
                Name = c.Name,
                Districts = c.Districts.Select(d => new DistrictModel()
                {
                    Id = d.Id,
                    GuId = d.GuId,
                    Name = d.Name
                }).ToList(),
                Users = c.Users.Select(u => new UserModel() { 
                    Id = u.Id,
                    GuId = u.GuId,
                    Name = u.Name,
                    Surname = u.Surname
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(CityModel model)
        {
            try
            {
                if (_cityRepository.Query().Any(c => c.Name.ToUpper() == model.Name.Trim().ToUpper()))
                    return new ErrorResult("Aynı şehir ikinci kez eklenemez!");
                var entity = new City()
                {
                    Name = model.Name.Trim()
                };
                _cityRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(CityModel model)
        {
            try
            {
                if (_cityRepository.Query().Any(c => c.Name.ToUpper() == model.Name.Trim().ToUpper() && c.Id != model.Id))
                    return new ErrorResult("Aynı şehir ikinci kez eklenemez!");
                var entity = _cityRepository.Query(c => c.GuId == model.GuId).SingleOrDefault();
                entity.Name = model.Name.Trim();
                _cityRepository.Update(entity);
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
                if (Query(c => c.GuId == guId).SingleOrDefault().Districts != null && Query(c => c.GuId == guId).SingleOrDefault().Districts.Count > 0)
                    return new ErrorResult("Bu şehir, altında ilçeler tanımlandığı için silinemez!");
                if (Query(c => c.GuId == guId).SingleOrDefault().Users != null && Query(c => c.GuId == guId).SingleOrDefault().Users.Count > 0)
                    return new ErrorResult("Bu şehirde kullanıcılarınız bulunduğu için şehri silemezsiniz!");
                _cityRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _cityRepository?.Dispose();
        }
    }
}
