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
    public class PostNumberService : IPostNumberService
    {
        private readonly PostNumberRepositoryBase _postNumberRepository;
        public PostNumberService(PostNumberRepositoryBase postNumberRepository)
        {
            _postNumberRepository = postNumberRepository;
        }

        public IQueryable<PostNumberModel> Query(Expression<Func<PostNumberModel, bool>> predicate = null)
        {
            var query = _postNumberRepository.Query("Users").Select(pN => new PostNumberModel()
            {
                Id = pN.Id,
                GuId = pN.GuId,
                Number = pN.Number,
                Users = pN.Users.Select(u => new UserModel()
                {
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

        public Result Add(PostNumberModel model)
        {
            try
            {
                if (Query().Any(pN => pN.Number == model.Number.Trim()))
                    return new ErrorResult("Bu posta numarası halihazırda bulunmaktadır!");
                var entity = new PostNumber()
                {
                    Number = model.Number.Trim()
                };
                _postNumberRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(PostNumberModel model)
        {
            try
            {
                if (Query().Any(pN => pN.Number == model.Number.Trim() && pN.Id != model.Id))
                    return new ErrorResult("Bu posta numarası hali hazırda bulunmaktadır!");
                var entity = _postNumberRepository.Query(pN => pN.GuId == model.GuId).SingleOrDefault();
                entity.Number = model.Number.Trim();
                _postNumberRepository.Update(entity);
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
                    return new ErrorResult("Kayıtlı kullanıcılarımızın bulunduğu bir posta numarasını silinemez!");
                _postNumberRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _postNumberRepository?.Dispose();
        }
    }
}
