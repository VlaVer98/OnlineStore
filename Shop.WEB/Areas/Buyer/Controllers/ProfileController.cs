using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Models.Dtos.User;
using Shop.WEB.Areas.Buyer.Controllers.Base;
using System;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Shop.WEB.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Shop.Domain.Contracts.Services.Response;

namespace Shop.WEB.Areas.Buyer.Controllers
{
    public class ProfileController : BaseBuyerController
    {
        public ProfileController(IServiceProvider service)
            : base(service) { }

        public IActionResult Index()
        {
            UserDto userDto = _services.GetService<IUserService>().Get(GetNameIdentifier());
            if (userDto == null)
                return BadRequest();

            UserViewModel userVM = _services.GetService<IMapper>()
                .Map<UserViewModel>(userDto);

            return View(userVM);
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangingPasswordViewModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangingPasswordViewModel changingPasswordVM)
        {
            if (ModelState.IsValid)
            {
                ServiceResponse serviceResponse = _services.GetService<IAccountService>()
                    .ChangePassword(GetNameIdentifier(), 
                    changingPasswordVM.OldPassword, 
                    changingPasswordVM.NewPassword);

                if (serviceResponse.IsSuccessful)
                    return RedirectToAction("Index");

                foreach (var item in serviceResponse.AllMessages)
                    ModelState.AddModelError("", item);
            }

            return View(changingPasswordVM);
        }

        public IActionResult Edit()
        {
            UserProfileDto userProfileDto = _services.GetService<IUserProfileService>()
                .GetForUser(GetNameIdentifier());
            UserProfileViewModel userProfileVM = _services.GetService<IMapper>()
                .Map<UserProfileViewModel>(userProfileDto);
            return View(userProfileVM);
        }

        [HttpPost]
        public IActionResult Edit(UserProfileViewModel userProfileVM)
        {
            if (ModelState.IsValid)
            {
                UserProfileDto userProfileDto = _services.GetService<IMapper>()
                    .Map<UserProfileDto>(userProfileVM);
                ServiceResponse serviceResponse = _services.GetService<IUserService>()
                    .UpdateProfile(GetNameIdentifier(), userProfileDto);
                if (!serviceResponse.IsSuccessful)
                    foreach (var item in serviceResponse.AllMessages)
                        ModelState.AddModelError("", item);
                }

            return View(userProfileVM);
        }
    }
}
