using LoginSignUp.Repository.Interface;
using LoginSignUp.Utils.Enums;
using LoginSignUp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginSignUp.Controllers
{
    public class AccountController : Controller
    {

        private IUsers UserService;
        public AccountController(IUsers users)
        {
            UserService = users;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInModel model)
        {
            var result = UserService.SignIn(model);
            if (result == SignInEnum.Success)
            {
                //A claim is a statement about a subject by an issuer and    
                //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, model.Email),
                };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                });
                return RedirectToAction("Index", "Home");
            }
            else if (result == SignInEnum.WrongCredentials)
            {
                ModelState.AddModelError(string.Empty, "InValid Login Credential");
            }
            else if (result == SignInEnum.NotVerified)
            {
                ModelState.AddModelError(string.Empty, "User Not Verified");
            }
            else if (result == SignInEnum.InActive)
            {
                ModelState.AddModelError(string.Empty, "Your Account is InActive");
            }

            return View(model);
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = UserService.SignUp(model);
                if (result == SignUpEnum.Success)
                {
                    return RedirectToAction("VerifyAccount");

                }
                else if (result == SignUpEnum.EmailExist)
                {
                    ModelState.AddModelError(string.Empty, "This Email already exist ,Please try another");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something Went wrong !..");
                }
            }
            // var result = UserService.SignUp(model);
            return View();
        }
        public IActionResult VerifyAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifyAccount(string otp)
        {
            if (otp != null)
            {
                if (UserService.VerifyAccount(otp))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct Otp");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Enter Otp");
            }
            return View();
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotpasswordModel model)
        {
            var res = UserService.ForgotPassword(model);
            if (res == ForgotPassEnum.Success)
            {
                return RedirectToAction("EnterOtpForm");

            }

            return View();
        }
        public IActionResult EnterOtpForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EnterOtpForm(ForgotOtpModel otp)
        {

            return RedirectToAction("Index","Home");
        }
    }
}
