﻿using BugTracker2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BugTracker2.Controllers
{
    public class AccountController : Controller
    {
        //Dependency Injection setup for AccountController
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Register(AppUser yousir)
        {
            try
            {
                //Try and find user from UserManager
                AppUser user = await _userManager.FindByNameAsync(yousir.UserName);

                //If it doesn't find the user, creates one with these properties
                if (user == null)
                {
                    //Change this to reflect validated user input
                    user = new AppUser
                    {
                        UserName = yousir.UserName,
                        Email = yousir.Email,
                        FirstName = yousir.FirstName,
                        LastName = yousir.LastName
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, "Test123!");
                    ViewBag.Message = "User was created";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        public async Task<IActionResult> Login(AppUser user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("BugBoard", "Bugs");
            }
            else
            {
                return RedirectToAction("LoginForm", "Account");
            }
        }
        public IActionResult LoginForm()
        {
            return View();
        }
        public IActionResult RegisterForm()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");             
        }
    }
}
