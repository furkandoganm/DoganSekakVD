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
    public class UserProductService : IUserProductService
    {
        private readonly UserProductRepositoryBase _userProductRepository;
        public UserProductService(UserProductRepositoryBase userProductRepository)
        {
            _userProductRepository = userProductRepository;
        }

        public IQueryable<UserProductModel> Query(Expression<Func<UserProductModel, bool>> predicate = null)
        {
            var query = _userProductRepository.Query().Select(uP => new UserProductModel()
            {
                Id = uP.Id,
                GuId = uP.GuId,
                IsBuy = uP.IsBuy,
                IsLike = uP.IsLike,
                Product = new ProductModel()
                {
                    Id = uP.Product.Id,
                    GuId = uP.Product.GuId,
                    Name = uP.Product.Name,
                    ImagePath = uP.Product.ImagePath,
                    Price = uP.Product.Price
                },
                User = new UserModel()
                {
                    Id = uP.User.Id,
                    GuId = uP.User.GuId,
                    Name = uP.User.Name,
                    Surname = uP.User.Surname,
                    EMail = uP.User.EMail
                }
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(UserProductModel model)
        {
            try
            {
                var entity = new UserProduct()
                {
                    IsBuy = model.IsBuy,
                    IsLike = model.IsLike,
                    ProductId = model.ProductId,
                    UserId = model.ProductId
                };
                _userProductRepository.Add(entity);
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
                _userProductRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _userProductRepository?.Dispose();
        }

        public Result Update(UserProductModel model)
        {
            try
            {
                var entity = new UserProduct()
                {
                    Id = model.Id,
                    GuId = model.GuId,
                    IsBuy = model.IsBuy,
                    IsLike = model.IsLike,
                    ProductId = model.ProductId,
                    UserId = model.ProductId
                };
                _userProductRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
    }
}
