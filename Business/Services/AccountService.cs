using AppCore.Bussiness.Models.Results;
using Business.Enums;
using Business.Models.AccountModels;
using Business.Services.Bases;
using DataAccess.Repositories.Bases;
using Entity.Entities;
using System;
using System.Linq;

namespace Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserRepositoryBase _userRepository;
        public AccountService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }
        public Result<LoginModel> Login(LoginModel model)
        {
            try
            {
                var user = _userRepository.Query("Role").SingleOrDefault(u => u.EMail.Trim() == model.EMail && u.Password.Trim() == model.Password);
                if (user == null)
                {
                    user = _userRepository.Query().SingleOrDefault(u => u.EMail.Trim() == model.EMail);
                    if (user != null)
                        return new ErrorResult<LoginModel>("Şifrenizin doğru olduğundan emin olun!");
                    //user = _userRepository.Query().SingleOrDefault(u => u.Password.Trim() == model.Password);
                    //if (user != null)
                    //    return new ErrorResult<LoginModel>("Mail adresinizin doğru olduğundan emin olun!");
                    return new ErrorResult<LoginModel>("Kullanıcılarımız arasında sizi göremiyoruz, şifranizi ve mail adresinizi kontrol ediniz!");
                }
                var loginModel = new LoginModel()
                {
                    Id = user.Id,
                    GuId = user.GuId,
                    Name = user.Name,
                    Surname = user.Surname,
                    Role = user.Role.Name
                };
                return new SuccessResult<LoginModel>(loginModel);
            }
            catch (Exception exc)
            {
                return new ExceptionResult<LoginModel>(exc);
            }
        }

        public Result<LoginModel> Register(RegisterModel model)
        {
            try
            {
                if (_userRepository.Query().Any(u => u.EMail == model.EMail.Trim()))
                    return new ErrorResult<LoginModel>("Mail adresiniz halihazırda bir kullanıcımıza aittir. Lütfen kullanıcımız olup olmadığınızdan emin olunuz!");
                var entity = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    EMail = model.EMail,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    RoleId = (int)Roles.User,
<<<<<<< HEAD
                    IsActive = true,
                    CityId = (int)UnknownRelation.CityId,
                    DistrictId = (int)UnknownRelation.DistrictId,
                    PostNumberId = (int)UnknownRelation.PostNumberId
=======
                    IsActive = true
>>>>>>> 5f1563d2e59cb457ac1d671bdb7cab2f1e00d5eb
                };
                _userRepository.Add(entity);
                var loginModel = new LoginModel()
                {
                    Id = entity.Id,
                    GuId = entity.GuId,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    Role = Roles.User.ToString()//Sorun Çıkabilir
                };
                return new SuccessResult<LoginModel>(loginModel);
            }
            catch (Exception exc)
            {
                return new ExceptionResult<LoginModel>(exc);
            }
        }

        public Result<RegisterModel> Update(RegisterModel model)
        {
            try
            {
                if (_userRepository.Query().Any(u => u.EMail == model.EMail.Trim() && u.Id != model.Id))
                    return new ErrorResult<RegisterModel>("Aynı mail adresine sahip kullanıcımız farklı bir kullanıcımız bulunmaktadır. Mail adresinizin size ait olduğundan emin olunuz!");
                var entity = _userRepository.Query(u => u.GuId == model.GuId).SingleOrDefault();
                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.Password = model.Password;
                entity.PhoneNumber = model.PhoneNumber;
                entity.PostNumberId = model.PostNumberId;
                entity.DistrictId = model.DistrictId;
                entity.CityId = model.CityId;
                _userRepository.Update(entity);
                return new SuccessResult<RegisterModel>(model);
            }
            catch (Exception exc)
            {
                return new ExceptionResult<RegisterModel>(exc);
            }
        }
    }
}
