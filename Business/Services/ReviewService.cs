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
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepositoryBase _reviewRepository;
        public ReviewService(ReviewRepositoryBase reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public IQueryable<ReviewModel> Query(Expression<Func<ReviewModel, bool>> predicate = null)
        {
            var query = _reviewRepository.Query().Select(r => new ReviewModel()
            {
                Id = r.Id,
                GuId = r.GuId,
                Explanation = r.Explanation,
                NumberofLikes = r.NumberofLikes,
                NumberofDislikes = r.NumberofDislikes,
                IsActive = r.IsActive,
                Product = new ProductModel()
                {
                    Id = r.Product.Id,
                    GuId = r.Product.GuId,
                    Name = r.Product.Name,
                    IsActive = r.Product.IsActive,
                    ImagePath = r.Product.ImagePath,
                    Price = r.Product.Price
                },
                User = new UserModel()
                {
                    Id = r.User.Id,
                    GuId = r.User.GuId,
                    Name = r.User.Name,
                    Surname = r.User.Surname,
                    IsActive = r.User.IsActive
                }
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(ReviewModel model)
        {
            try
            {
                var entity = new Review()
                {
                    Explanation = model.Explanation,
                    NumberofLikes = model.NumberofLikes,
                    NumberofDislikes = model.NumberofDislikes,
                    IsActive = model.IsActive,
                    ProductId = model.ProductId,
                    UserId = model.UserId
                };
                _reviewRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(ReviewModel model)
        {
            try
            {
                var entity = new Review()
                {
                    Id = model.Id,
                    GuId = model.GuId,
                    Explanation = model.Explanation,
                    NumberofLikes = model.NumberofLikes,
                    NumberofDislikes = model.NumberofDislikes,
                    IsActive = model.IsActive,
                    ProductId = model.ProductId,
                    UserId = model.UserId
                };
                _reviewRepository.Update(entity);
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
                _reviewRepository.Delete(guId);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _reviewRepository?.Dispose();
        }
    }
}
