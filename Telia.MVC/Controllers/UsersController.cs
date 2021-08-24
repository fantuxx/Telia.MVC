﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telia.MVC.Data;

namespace Telia.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userMoManager;

        public UsersController(ApplicationDbContext context, UserManager<User>userMoManager)
        {
            _context = context;
            _userMoManager = userMoManager;
        }

        public async Task<IActionResult> Index() // show All Users
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            ViewData["Roles"] = new SelectList("Admin", "Moderator", "User");

            var model = new UserUpdateModel
            {
                Name = user.Name,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Id = user.Id,
                Role = UserUpdateModel.Roles.User,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name, LastName,PhoneNumber, Email,  EmailConfirmed,PhoneNumberConfirmed ,LockoutEnabled ,TwoFactorEnabled, Role ")]UserUpdateModel ModelFromView)
        {
           
            //admin id 6fe73b67-e277-4079-8deb-a1497945a0b6

            if (ModelState.IsValid)
            {
                
                    var user = await _context.Users.FindAsync(id);

                    user.LastName = ModelFromView.LastName;
                    user.Name = ModelFromView.Name;
                    user.PhoneNumber = ModelFromView.PhoneNumber;
                    user.Email = ModelFromView.Email;
                    user.EmailConfirmed = ModelFromView.EmailConfirmed;
                    user.PhoneNumberConfirmed = ModelFromView.PhoneNumberConfirmed;
                    user.LockoutEnabled = ModelFromView.LockoutEnabled;
                    user.TwoFactorEnabled = ModelFromView.TwoFactorEnabled;

                    _context.Update(user);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));


            }

            return View(ModelFromView);

        }

        private bool UsserExists(string userId)
        {
            return _context.Users.Any(e => e.Id == userId);

        }


        public IActionResult Delete(string id)
        {
            return View();
        }

        public IActionResult View(string Id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telia.MVC.Areas.Identity.Pages.Account.RegisterModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    NormalizedUserName = model.Email.ToUpper(),
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = null,
                    ConcurrencyStamp = null,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Name = model.Name,
                    LastName = model.LastName,
                };
                var result = await _userMoManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(model);
            }
            //i If we got so far
            return NotFound();


        }
    }
}