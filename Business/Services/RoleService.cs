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
    public class RoleService: IRoleService
    {
        private readonly RoleRepositoryBase _roleRepository;
        public RoleService(RoleRepositoryBase roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IQueryable<RoleModel> Query(Expression<Func<RoleModel, bool>> predicate = null)
        {
            var query = _roleRepository.Query().Select(r => new RoleModel
            {
                Id = r.Id,
                GuId = r.GuId,
                Name = r.Name
            });
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Result Add(RoleModel model)
        {
            try
            {
                if (_roleRepository.Query().Any(r => r.Name.ToUpper() == model.Name.Trim().ToUpper()))
                    return new ErrorResult("Bu rol halihazırda kullanılmaktadır!");
                var entity = new Role()
                {
                    Name = model.Name
                };
                _roleRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Update(RoleModel model)
        {
            try
            {
                if (_roleRepository.Query().Any(r => r.Name.ToUpper() == model.Name.Trim().ToUpper() && r.Id != model.Id))
                    return new ErrorResult("Bu rol halihazırda kullanılmaktadır!");
                var entity = new Role()
                {
                    Id = model.Id,
                    GuId = model.GuId,
                    Name = model.Name
                };
                _roleRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Delete(string guId)
        {
            if (_roleRepository.Query().SingleOrDefault(r => r.GuId == guId).Users != null)
                return new ErrorResult("Altında kullanıcıları bulunduğu için bu rol silinemez");
            _roleRepository.Delete(guId);
            return new SuccessResult();
        }

        public void Dispose()
        {
            _roleRepository?.Dispose();
        }
    }
}
