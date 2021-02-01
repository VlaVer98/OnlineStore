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

namespace Shop.WEB.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IServiceProvider _services;

        public RegistrationController(IServiceProvider services)
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
                ServiceResponse<List<string>> serviceResponse = _services.GetService<IAccountService>()
                    .CreateBuyer(buyerRegistrationDto);
                if (serviceResponse.IsSuccessful)
                {
                    return Redirect("/home/index");
                }

                foreach (var item in serviceResponse.ResponseObject)
                {
                    ModelState.AddModelError("", item);
                }
            }

            return View(buyerRegistrationVM);
        }
    }
}
