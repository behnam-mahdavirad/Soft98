using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore;
using Soft98.Core.Classes;
using Soft98.Core.Interfaces;
using Soft98.Core.ViewModels;
using Soft98.DataAccessLayer.Entities;

namespace Soft98.Controllers
{

    public class AccountController : Controller
    {
        private IUser _iuser;
        private readonly IRecaptchaService _recaptcha;

        public AccountController(IUser iuser, IRecaptchaService recaptcha)
        {
            _iuser = iuser;
            _recaptcha = recaptcha;
        } // end constructor AccountController

        public IActionResult Register()
        {
            return View();
        } // end IActionResult Register

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var recaptcha = await _recaptcha.Validate(Request);
                if (!recaptcha.success)
                {
                    ModelState.AddModelError("Mobile", "لطفا تیک را بزنید");
                    return View(register);
                }
                else
                {
                    if (_iuser.IsMobileNumberExists(register.Mobile))
                    {
                        ModelState.AddModelError("Mobile", "شما قبلا ثبت نام کرده اید");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        User user = new User()
                        {
                            IsActive = false,
                            Mobile = register.Mobile,
                            Code = CodeGenerator.ActiveCode(),
                            Password = HashGenerator.EncodingPassWithMd5(register.Password),
                            IdRole = 2 // User
                        };

                        _iuser.AddUser(user);

                        return RedirectToAction("Active");
                    }
                }
            }
            else
            {
                return View(register);
            }
        } // end method Register

        public IActionResult Login()
        {
            return View();
        } // end method Login

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _iuser.LoginUser(login.Mobile, login.Password);

                if (user != null)
                {
                    if (user.IsActive)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Active");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Password", "مشخصات کاربری صحیح نمی باشد");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }

        } // end IActionResult Login

    } // end public class AccountController : Controller

} // end namespace Soft98.Controllers