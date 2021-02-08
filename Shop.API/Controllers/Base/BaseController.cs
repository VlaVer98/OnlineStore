using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Shop.API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IServiceProvider _services;

        public BaseController(IServiceProvider service)
        {
            _services = service;
        }

        protected Guid GetNameIdentifier()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) != null ?
                new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
                : Guid.Empty;
        }
    }
}
