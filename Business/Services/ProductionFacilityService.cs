using AppCore.Bussiness.Models.Results;
using Business.Models;
using Business.Services.Bases;
using DataAccess.Repositories.Bases;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductionFacilityService : IProductionFacilityService
    {
        private readonly ProductionFacilityRepositoryBase _productionFacilityRepository;
        private readonly ProductionFacilityProductRepositoryBase _productionFacilityProductRepository;
        public ProductionFacilityService(ProductionFacilityRepositoryBase productionFacilityRepository, ProductionFacilityProductRepositoryBase productionFacilityProductRepository)
        {
            _productionFacilityRepository = productionFacilityRepository;
            _productionFacilityProductRepository = productionFacilityProductRepository;
        }

        public IQueryable<ProductionFacilityModel> Query(Expression<Func<ProductionFacilityModel, bool>> predicate = null)
        {
            var query = _productionFacilityRepository.Query().Select(pF => new ProductionFacilityModel()
            {
                Id = pF.Id,
                GuId = pF.GuId,
                Name = pF.Name,
                Capacit = pF.Capacit,
                ImagePath = pF.ImagePath,
                Location = pF.Location,
                City = new CityModel()
                {
                    Id = pF.City.Id,
                    GuId = pF.City.GuId,
                    Name = pF.City.Name
                },
                District = new DistrictModel()
                {
                    Id = pF.District.Id,
                    GuId = pF.District.GuId,
                    Name = pF.District.Name
                },
                Products = pF.Products.Select(pFP => new ProductionFacilityProductModel()
                {
                    Id = pFP.Id,
                    GuId = pFP.GuId,
                    ProductId = pFP.ProductId
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(ProductionFacilityModel model)
        {
            try
            {
                if (_productionFacilityRepository.Query().Any(pF => pF.Name == model.Name.Trim()))
                    return new ErrorResult("Aynı isimli Üretim tesisimiz bulunmaktadır!");
                var entity = new ProductionFacility()
                {
                    Name = model.Name.Trim(),
                    CityId = model.CityId,
                    DistrictId = model.DistrictId,
                    ImagePath = model.ImagePath,
                    Capacit = model.Capacit,
                    Location = model.Location.Trim()
                };
                _productionFacilityRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
        public Result Update(ProductionFacilityModel model)
        {
            try
            {
                if (_productionFacilityRepository.Query().Any(pF => pF.Name == model.Name.Trim() && pF.GuId != model.GuId))
                    return new ErrorResult("Aynı isimli Üretim tesisimiz bulunmaktadır!");
                var entity = new ProductionFacility()
                {
                    Id = model.Id,
                    GuId = model.GuId,
                    Name = model.Name.Trim(),
                    CityId = model.CityId,
                    DistrictId = model.DistrictId,
                    ImagePath = model.ImagePath,
                    Capacit = model.Capacit,
                    Location = model.Location.Trim()
                };
                _productionFacilityRepository.Update(entity);
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
                if (Query(pF => pF.GuId == guId).SingleOrDefault().Products != null && Query(pF => pF.GuId == guId).SingleOrDefault().Products.Count > 0)
                    return new ErrorResult("Bu üretim tesisinde üretilmiş ürünler bulundukça tesis silinemez!");
                _productionFacilityRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _productionFacilityRepository?.Dispose();
        }
    }
}
