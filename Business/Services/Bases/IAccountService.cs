using AppCore.Bussiness.Models.Results;
using Business.Models.AccountModels;

namespace Business.Services.Bases
{
    public interface IAccountService
    {
        Result<LoginModel> Register(RegisterModel model);
        Result<LoginModel> Login(LoginModel model);
        Result<RegisterModel> Update(RegisterModel model);
    }
}
