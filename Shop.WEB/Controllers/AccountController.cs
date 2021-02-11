using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Models.Identity;
using Shop.WEB.Models.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models.Dtos.Account;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Contracts.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

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

        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}
