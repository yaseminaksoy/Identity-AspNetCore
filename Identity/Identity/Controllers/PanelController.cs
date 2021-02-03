﻿using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public PanelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel
            {
                Email=user.Email,
                Name=user.Name,
                Surname=user.Surname,
                PhoneNumber=user.PhoneNumber,
                PictureUrl=user.PictureUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(model.Picture != null)
                {
                    // directory/wwwroot/img/pictureName.extension
                    var directory = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(model.Picture.FileName);
                    var pictureName = Guid.NewGuid() + extension;
                    var path = directory + "/wwwroot/img/" + pictureName;
                    using var stream = new FileStream(path, FileMode.Create);

                    await model.Picture.CopyToAsync(stream);
                    user.PictureUrl = pictureName;
                }
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return View(model);
        }
        // to make actionresult method open to everybody [AllowAnonymous]

    }
}
