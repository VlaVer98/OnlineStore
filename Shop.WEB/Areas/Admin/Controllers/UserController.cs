﻿using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.User;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;
using Shop.Domain.Contracts.Services.Response;

namespace Shop.WEB.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        public UserController(IServiceProvider services)
            : base(services) { }

        public IActionResult Index()
        {
            IEnumerable<UserDto> usersDto = _services.GetService<IUserService>().GetAll();
            IEnumerable<UserViewModel> users = _services.GetService<IMapper>()
                .Map<IEnumerable<UserViewModel>>(usersDto);

            return View(users);
        }

        public IActionResult Details(Guid id)
        {
            UserDto userDto = _services.GetService<IUserService>().Get(id);
            UserViewModel userVm = _services.GetService<IMapper>()
                .Map<UserViewModel>(userDto);

            return View(userVm);
        }

        public IActionResult EditProfile(Guid id) 
        {
            UserProfileDto userProfileDto = _services.GetService<IUserProfileService>()
                .Get(id);
            if(userProfileDto == null)
                return BadRequest();

            UserProfileViewModel userVM = _services.GetService<IMapper>()
                .Map<UserProfileViewModel>(userProfileDto);
            return View(userVM);
        }

        [HttpPost]
        public IActionResult EditProfile(UserProfileViewModel userProfileVM)
        {
            if (ModelState.IsValid)
            {
                UserProfileDto userProfileDto = _services.GetService<IMapper>()
                    .Map<UserProfileDto>(userProfileVM);
                ServiceResponse serviceResponse = _services.GetService<IUserProfileService>()
                    .Update(userProfileDto);
                if (!serviceResponse.IsSuccessful)
                {
                    foreach (var item in serviceResponse.AllMessages)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }

            return View(userProfileVM);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            ServiceResponse serviceResponse = _services.GetService<IUserService>()
                .Delete(id);
            if (serviceResponse.IsSuccessful)
                return RedirectToAction("index");

            foreach (var item in serviceResponse.AllMessages)
                ModelState.AddModelError("", item);

            return View();
        }
    }
}
