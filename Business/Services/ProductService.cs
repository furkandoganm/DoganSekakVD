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
    public class ProductService : IProductService
    {
        private readonly ProductRepositoryBase _productRepository;
        private readonly ProductionFacilityProductRepositoryBase _productionFacilityProductRepository;
        public ProductService(ProductRepositoryBase productRepository, ProductionFacilityProductRepositoryBase productionFacilityProductRepository)
        {
            _productRepository = productRepository;
            _productionFacilityProductRepository = productionFacilityProductRepository;
        }

        public IQueryable<ProductModel> Query(Expression<Func<ProductModel, bool>> predicate = null)
        {
            var query = _productRepository.Query("Users", "Category", "Reviews").Select(p => new ProductModel()
            {
                Id = p.Id,
                GuId = p.GuId,
                Name = p.Name,
                Explanation = p.Explanation,
                ImagePath = p.ImagePath,
                Price = p.Price,
                ProductionDate = p.ProductionDate,
                ProductionCost = p.ProductionCost,
                SerialNumber = p.SerialNumber,
                StockAmount = p.StockAmount,
                NumberofVisitsPerMonth = p.NumberofVisitsPerMonth,
                IsActive = p.IsActive,
                Category = new CategoryModel()
                {
                    Id = p.Category.Id,
                    GuId = p.Category.GuId,
                    Name = p.Category.Name
                },
                Reviews = p.Reviews.Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    GuId = r.GuId,
                    Explanation = r.Explanation
                }).ToList(),
                Users = p.Users.Select(uP => new UserProductModel()
                {
                    Id = uP.Id,
                    GuId = uP.GuId,
                    IsBuy = uP.IsBuy,
                    IsLike = uP.IsLike
                }).ToList(),
                ProductionFacilities = p.ProductionFacilities.Select(pFP => new ProductionFacilityProductModel()
                {
                    Id = pFP.Id,
                    GuId = pFP.GuId,
                    ProductionFacilityId = pFP.ProductionFacilityId
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(ProductModel model)
        {
            try
            {
                if (_productRepository.Query().Any(p => p.Name == model.Name.Trim()))
                    return new ErrorResult("Aynı isimli ürün ikinci kez eklenemez!");
                //if (_productRepository.Query().Any(p => p.SerialNumber == model.SerialNumber.Trim()))
                //    return new ErrorResult("Aynı seri numaralı ürün ikinci kez eklenemez!");
                var entity = new Product()
                {
                    Name = model.Name.Trim(),
                    Explanation = model.Explanation.Trim(),
                    SerialNumber = model.SerialNumber.Trim(),
                    ProductionCost = model.ProductionCost,
                    ProductionDate = model.ProductionDate,
                    ProductionFacilities = model.ProductionFacilities.Select(pFP => new ProductionFacilityProduct()
                    {
                        Id = pFP.Id,
                        GuId = pFP.GuId,
                        ProductionFacilityId = pFP.Id
                    }).ToList(),
                    CategoryId = model.CategoryId,
                    ImagePath = model.ImagePath,
                    IsActive = true,
                    Price = model.Price,
                    StockAmount = model.StockAmount
                };
                _productRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(ProductModel model)
        {
            try
            {
                if (_productRepository.Query().Any(p => p.Name == model.Name.Trim() && p.GuId != model.GuId))
                    return new ErrorResult("Aynı isimli ürün ikinci kez eklenemez!");
                //if (_productRepository.Query().Any(p => p.SerialNumber == model.SerialNumber.Trim() && p.GuId != model.GuId))
                //    return new ErrorResult("Aynı seri numaralı ürün ikinci kez eklenemez!");
                var entity = _productRepository.Query(p => p.GuId == model.GuId).SingleOrDefault();
                entity.Name = model.Name.Trim();
                entity.Explanation = model.Explanation.Trim();
                entity.SerialNumber = model.SerialNumber.Trim();
                entity.ProductionCost = model.ProductionCost;
                entity.ProductionDate = model.ProductionDate;
                entity.ProductionFacilities = model.ProductionFacilities.Select(pFP => new ProductionFacilityProduct()
                {
                    Id = pFP.Id,
                    GuId = pFP.GuId,
                    ProductionFacilityId = pFP.Id
                }).ToList();
                entity.CategoryId = model.CategoryId;
                entity.ImagePath = model.ImagePath;
                entity.IsActive = true;
                entity.Price = model.Price;
                entity.StockAmount = model.StockAmount;
                entity.IsActive = model.IsActive;
                _productRepository.Update(entity);
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
                var entity = Query(p => p.GuId == guId).SingleOrDefault();
                foreach (var item in entity.ProductionFacilities)
                {
                    _productionFacilityProductRepository.Delete(item.GuId);
                }
                _productRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
        public Result FakeDelete(string guId)
        {
            try
            {
                var entity = _productRepository.Query(p => p.GuId == guId).SingleOrDefault();
                entity.IsActive = false;
                _productRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
