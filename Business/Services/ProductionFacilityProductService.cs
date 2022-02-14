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
    public class ProductionFacilityProductService: IProductionFacilityProductService
    {
        private readonly ProductionFacilityProductRepositoryBase _productionFacilityProductRepository;
        public ProductionFacilityProductService(ProductionFacilityProductRepositoryBase productionFacilityProductRepository)
        {
            _productionFacilityProductRepository = productionFacilityProductRepository;
        }

        public IQueryable<ProductionFacilityProductModel> Query(Expression<Func<ProductionFacilityProductModel, bool>> predicate = null)
        {
            var query = _productionFacilityProductRepository.Query().Select(pFP => new ProductionFacilityProductModel()
            {
                Id = pFP.Id,
                GuId = pFP.GuId,
                Product = new ProductModel()
                {
                    Id = pFP.Product.Id,
                    GuId = pFP.Product.GuId,
                    Name = pFP.Product.Name,
                    Explanation = pFP.Product.Explanation,
                    Price = pFP.Product.Price
                },
                ProductionFacility = new ProductionFacilityModel()
                {
                    Id = pFP.ProductionFacility.Id,
                    GuId = pFP.ProductionFacility.GuId,
                    Name = pFP.ProductionFacility.Name,
                    Location = pFP.ProductionFacility.Location
                }
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(ProductionFacilityProductModel model)
        {
            try
            {
                var entity = new ProductionFacilityProduct()
                {
                    ProductId = model.ProductId,
                    ProductionFacilityId = model.ProductionFacilityId
                };
                _productionFacilityProductRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
        public Result Update(ProductionFacilityProductModel model)
        {
            try
            {
                var entity = new ProductionFacilityProduct()
                {
                    Id = model.Id,
                    GuId = model.GuId,
                    ProductId = model.ProductId,
                    ProductionFacilityId = model.ProductionFacilityId
                };
                _productionFacilityProductRepository.Update(entity);
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
                _productionFacilityProductRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _productionFacilityProductRepository?.Dispose();
        }
    }
}
