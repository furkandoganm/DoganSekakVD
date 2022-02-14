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
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepositoryBase _categoryRepository;
        public CategoryService(CategoryRepositoryBase categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IQueryable<CategoryModel> Query(Expression<Func<CategoryModel, bool>> predicate = null)
        {
            var query = _categoryRepository.Query("Products").Select(c => new CategoryModel()
            {
                Id = c.Id,
                GuId = c.GuId,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductModel()
                {
                    Id = p.Id,
                    GuId = p.GuId,
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                    Explanation = p.Explanation
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(CategoryModel model)
        {
            try
            {
                if (_categoryRepository.Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper()))
                    return new ErrorResult("Aynı isimli bir başka kategoriniz bulunmaktadır!");
                var entity = new Category()
                {
                    Name = model.Name.Trim()
                };
                _categoryRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(CategoryModel model)
        {
            try
            {
                if (_categoryRepository.Query().Any(c => c.Name.ToUpper() == model.Name.Trim().ToUpper() && c.Id != model.Id))
                    return new ErrorResult("Aynı isimli bir başka kategoriniz bulunmaktadır!");
                var entity = _categoryRepository.Query(c => c.GuId == model.GuId).SingleOrDefault();
                entity.Name = model.Name;
                _categoryRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Delete(string guId)
        {
            var entity = Query(C => C.GuId == guId).SingleOrDefault();
            if (entity.Products != null && entity.Products.Count() > 0)
                return new ErrorResult("Bu kategori, altında ürün barındırması hasebiyle silinemez!");
            _categoryRepository.Delete(guId);
            return new SuccessResult();
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
