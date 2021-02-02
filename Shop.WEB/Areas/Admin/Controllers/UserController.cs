using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.User;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

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
    }
}
