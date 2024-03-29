﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Data.DB.Maps;
using Shop.Domain.Models.Entities;
using Shop.Domain.Models.Identity;
using System;
using System.IO;

namespace Shop.Data.DB.Context
{
    public class ShopDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserProfileMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new OrderProductMap());

            modelBuilder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role { 
                        Id = new Guid("515f47dc-c4f1-4041-a385-bc6b0991f517"), 
                        Name =  Domain.Constants.UserRoles.Admin,
                        NormalizedName = Domain.Constants.UserRoles.Admin.ToUpper(),
                        ConcurrencyStamp = "515f47dc-c4f1-4041-a385-bc6b0991f517",
                    },
                    new Role {
                        Id = new Guid("1153afda-1fbd-446a-b37d-6b62ffbe3204"),
                        Name =  Domain.Constants.UserRoles.Buyer,
                        NormalizedName = Domain.Constants.UserRoles.Buyer.ToUpper(),
                        ConcurrencyStamp = "1153afda-1fbd-446a-b37d-6b62ffbe3204",
                    }
                });
        }
    }
}
