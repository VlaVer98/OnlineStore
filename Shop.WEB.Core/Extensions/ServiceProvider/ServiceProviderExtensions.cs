﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.DB;
using Shop.Data.DB.Context;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Services;
using Shop.WEB.Core.AutoMapper;

namespace Shop.WEB.Core.Extensions.ServiceProvider
{
    public static class ServiceProviderExtensions
    {
        public static void AddShopService(this IServiceCollection services, string connectionDB)
        {
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(
                    connectionDB,
                    x => x.MigrationsAssembly("Shop.Data.DB")));
            services.AddIdentityCore<User>().AddEntityFrameworkStores<ShopDbContext>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ICartService, CartService>();
        }
    }
}
