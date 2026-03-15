﻿using BL.Managers.UserManager;
using BL.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Advanced_Company_Management_System__MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager manage;

        public AccountController(IUserManager UserManage)
        {
            manage = UserManage;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("LoginForm");
        }

        #region Registeration

        [HttpGet]
        public IActionResult RegisterForm()
        {
            // If already logged in, redirect to dashboard
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("GetAllcomp", "CompanyDet");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM ModelForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = manage.AddUser(ModelForm);
                    if (newUser != null)
                    {
                        TempData["SuccessMessage"] = "Registration successful! Please login with your credentials.";
                        return RedirectToAction("LoginForm");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed. Please try again.");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                }
            }

            return View("RegisterForm", ModelForm);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult LoginForm()
        {
            // If already logged in, redirect to dashboard
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("GetAllcomp", "CompanyDet");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM Model)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginForm", Model);
            }

            try
            {
                var user = manage.SignIn(Model);

                if (user != null)
                {
                    // Create claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", user.FullName ?? user.Username)
                    };

                    // Add role claims (if roles exist)
                    if (user.Roles != null && user.Roles.Any())
                    {
                        foreach (var role in user.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }

                    var claimsIdentity = new ClaimsIdentity(
                        claims, 
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // Remember me
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    TempData["SuccessMessage"] = $"Welcome back, {user.FullName ?? user.Username}!";
                    return RedirectToAction("GetAllcomp", "CompanyDet");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password. Please try again.");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
            }

            return View("LoginForm", Model);
        }
        #endregion 

        #region Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("LoginForm");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogoutGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("LoginForm");
        }
        #endregion
    }
}
