using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Models.Identity;
using Shop.WEB.Models.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models.Dtos.Account;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Shop.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceProvider _services;

        public AccountController(IServiceProvider services)
        {
            _services = services;
        }
        public IActionResult Create()
        {
            return View(new BuyerRegistrationViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BuyerRegistrationViewModel buyerRegistrationVM)
        {
            if (ModelState.IsValid)
            {
                BuyerRegistrationDto buyerRegistrationDto = _services.GetService<IMapper>()
                    .Map<BuyerRegistrationDto>(buyerRegistrationVM);
                ServiceResponse<User> serviceResponse = _services.GetService<IAccountService>()
                    .CreateBuyer(buyerRegistrationDto);

                if (serviceResponse.IsSuccessful)
                {
                    await _services.GetService<SignInManager<User>>()
                        .SignInAsync(serviceResponse.ResponseObject, false);
                    return Redirect("/home/index");
                }

                foreach (var item in serviceResponse.AllMessages)
                {
                    ModelState.AddModelError("", item);
                }
            }

            return View(buyerRegistrationVM);
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                User user = _services.GetService<IUserService>().GetByEmail(loginVM.Email);
                var result = await _services.GetService<SignInManager<User>>()
                    .PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginVM.ReturnUrl) && Url.IsLocalUrl(loginVM.ReturnUrl))
                        return Redirect(loginVM.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and(or) password");
                }
            }

            return View(loginVM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _services.GetService<SignInManager<User>>().SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
