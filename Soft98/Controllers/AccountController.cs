using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            ViewBag.Myst = 0;
            ViewBag.ModalTitle = "";
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
                    ViewBag.Myst = 0;
                    return View(register);
                }
                else
                {
                    if (_iuser.IsMobileNumberExists(register.Mobile))
                    {
                        ModelState.AddModelError("Mobile", "شما قبلا ثبت نام کرده اید");
                        //return RedirectToAction("Login");
                        ViewBag.Myst = 1;
                        ViewBag.ModalTitle = "ورود به سایت";
                        return View();
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

                        SMS sms = new SMS();
                        sms.Send(user.Mobile, "ثبت نام شما انجام شد، کد فعالسازی : " + user.Code);

                        //return RedirectToAction("Active");
                        ViewBag.Myst = 2;
                        ViewBag.ModalTitle = "فعالسازی حساب کاربری";
                        return View();

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
                        var Claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.Mobile)
                        };
                        var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(principle);
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

        public IActionResult Active()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Active(ActiveViewModel active)
        {
            if (ModelState.IsValid)
            {
                User user = _iuser.ActiveUser(active.Code);

                if (user != null)
                {
                    var Claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.Mobile)
                        };
                    var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principle = new ClaimsPrincipal(identity);

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Code", "کد فعالسازی صحیح نمی باشد");
                    return View(active);
                }
            }
            else
            {
                return View(active);
            }
        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget(ForgetViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = _iuser.ForgetPassword(forget.Mobile);

                if (user != null)
                {
                    SMS sms = new SMS();
                    sms.Send(forget.Mobile, "کد تایید برای فراموشی کلمه عبور " + user.Code + "می باشد");
                    return RedirectToAction("Reset");
                }

                else
                {
                    ModelState.AddModelError("Mobile", "این شماره موبایل هنوز ثبت نام نشده است");
                    return View(forget);
                }
            }
            else
            {
                return View(forget);
            }
        }

        public IActionResult Reset()
        {
            ViewBag.Myst = 0;
            ViewBag.ModalTitle = "";
            return View();
        }

        [HttpPost]
        public IActionResult Reset(ResetViewModel reset)
        {
            if (ModelState.IsValid)
            {
                if (_iuser.ResetPassword(reset.Code, reset.Password))
                {
                    ViewBag.Myst = 1;
                    ViewBag.ModalTitle = "ورود به سایت";
                    return View();
                }
                else
                {
                    ViewBag.Myst = 0;
                    ViewBag.ModalTitle = "";

                    ModelState.AddModelError("Code", "کد وارد شده صحیح نمی باشد");
                    return View(reset);
                }
            }
            else
            {
                return View(reset);
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index"); 
        }

    } // end public class AccountController : Controller

} // end namespace Soft98.Controllers