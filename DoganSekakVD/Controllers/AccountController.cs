using AppCore.Bussiness.Models.Results.Enums;
using Business.Models.AccountModels;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DoganSekakVD.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        public IActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var accountResult = _accountService.Register(model);
                switch (accountResult.Status)
                {
                    case ResultStatus.Error:
                        ModelState.AddModelError("", accountResult.Message);
                        break;
                    case ResultStatus.Exception:
                        return RedirectToAction("Error", "Home");
                    default:
                        break;
                }
                LoginModel tempModel = new LoginModel()
                {
                    EMail = model.EMail,
                    Password = model.Password
                };
                var result = _accountService.Login(tempModel);
                if (result.Status == ResultStatus.Exception)
                    return RedirectToAction("Error", "Home");
                if (result.Status == ResultStatus.Success)
                {
                    var user = result.Data;
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Sid, user.GuId.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.Login(model);
                if (result.Status == ResultStatus.Exception)
                    return RedirectToAction("Error", "Home");
                if (result.Status == ResultStatus.Success)
                {
                    var user = result.Data;
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Sid, user.GuId.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Details()
        {
            var userGuId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var model = _userService.Query().SingleOrDefault(u => u.GuId == userGuId);
            return View(model);
        }
        public IActionResult Edit()
        {
            var userGuId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var model = _userService.Query().SingleOrDefault(u => u.GuId == userGuId);
            RegisterModel registerModel = new RegisterModel()
            {
                Id = model.Id,
                GuId = model.GuId,
                Name = model.Name,
                Surname = model.Surname,
                EMail = model.EMail,
                Password = model.Password
            };
            return View(registerModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var userGuId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                registerModel.Id = _userService.Query(u => u.GuId == userGuId).SingleOrDefault().Id;
                var result = _accountService.Update(registerModel);
                if (result.Status == ResultStatus.Exception)
                    return RedirectToAction("Error", "Home");
                if (result.Status == ResultStatus.Success)
                    return RedirectToAction(nameof(Details));
                ModelState.AddModelError("", result.Message);
            }
            return View(registerModel);
        }
    }
}
