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
    public class UserService : IUserService
    {
        private readonly UserRepositoryBase _userRepositoryBase;
        private readonly UserProductRepositoryBase _userProductRepositoryBase;
        private readonly ReviewRepositoryBase _reviewRepositoryBase;
        public UserService(UserRepositoryBase userRepositoryBase, UserProductRepositoryBase userProductRepositoryBase, ReviewRepositoryBase reviewRepositoryBase)
        {
            _userRepositoryBase = userRepositoryBase;
            _userProductRepositoryBase = userProductRepositoryBase;
            _reviewRepositoryBase = reviewRepositoryBase;
        }

        public IQueryable<UserModel> Query(Expression<Func<UserModel, bool>> predicate = null)
        {
            var query = _userRepositoryBase.Query().Select(u => new UserModel()
            {
                Id = u.Id,
                GuId = u.GuId,
                Name = u.Name,
                Surname = u.Surname,
                EMail = u.EMail,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber,
                IsActive = u.IsActive,
                Address = u.Address,
                VisitFrequency = u.VisitFrequency,
                City = new CityModel()
                {
                    Id = u.City.Id,
                    GuId = u.City.GuId,
                    Name = u.City.Name
                },
                District = new DistrictModel()
                {
                    Id = u.District.Id,
                    GuId = u.District.GuId,
                    Name = u.District.Name
                },
                PostNumber = new PostNumberModel()
                {
                    Id = u.PostNumber.Id,
                    GuId = u.PostNumber.GuId,
                    Number = u.PostNumber.Number
                },
                Role = new RoleModel()
                {
                    Id = u.Role.Id,
                    GuId = u.Role.GuId,
                    Name = u.Role.Name
                },
                Products = u.Products.Select(p => new UserProductModel()
                {
                    Id = p.Id,
                    GuId = p.GuId,
                    IsBuy = p.IsBuy,
                    IsLike = p.IsLike,
                    ProductId = p.ProductId,
                    UserId = p.UserId
                }).ToList(),
                Reviews = u.Reviews.Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    GuId = r.GuId,
                    Explanation = r.Explanation,
                    IsActive = r.IsActive,
                    NumberofLikes = r.NumberofLikes,
                    NumberofDislikes = r.NumberofDislikes,
                    ProductId = r.Product.Id
                }).ToList()
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(UserModel model)
        {
            try
            {
                if (_userRepositoryBase.Query().Any(u => u.EMail == model.EMail))
                    return new ErrorResult("Bu mail adresine ait kullanıcı kaydı bulunmaktadır!");
                var entity = new User()
                {
                    EMail = model.EMail,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    IsActive = model.IsActive,
                    PhoneNumber = model.PhoneNumber,
                    VisitFrequency = model.VisitFrequency,
                    CityId = model.CityId,
                    DistrictId = model.DistrictId,
                    PostNumberId = model.PostNumberId,
                    RoleId = model.RoleId
                };
                _userRepositoryBase.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(UserModel model)
        {
            try
            {
                if (_userRepositoryBase.Query().Any(u => u.EMail == model.EMail && u.GuId != model.GuId))
                    return new ErrorResult("Bu mail adresine ait kullanıcı kaydı bulunmaktadır!");
                var entity = _userRepositoryBase.Query(u => u.GuId == model.GuId).SingleOrDefault();
                entity.EMail = model.EMail;
                entity.Password = model.Password;
                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.IsActive = model.IsActive;
                entity.PhoneNumber = model.PhoneNumber;
                entity.VisitFrequency = model.VisitFrequency;
                entity.CityId = model.CityId;
                entity.DistrictId = model.DistrictId;
                entity.PostNumberId = model.PostNumberId;
                entity.RoleId = model.RoleId;
                _userRepositoryBase.Update(entity);
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
                if (_userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Products != null && _userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Products.Count > 0)
                {
                    foreach (var item in _userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Products)
                    {
                        _userProductRepositoryBase.Delete(item.GuId, false);
                    }
                    _userProductRepositoryBase.Save();
                }
                if (_userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Reviews != null && _userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Reviews.Count > 0)
                {
                    foreach (var item in _userRepositoryBase.Query(u => u.GuId == guId).SingleOrDefault().Reviews)
                    {
                        _reviewRepositoryBase.Delete(item.GuId, false);
                    }
                    _reviewRepositoryBase.Save();
                }
                _userRepositoryBase.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _userRepositoryBase?.Dispose();
        }
    }
}
