using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Entities;
using System;

namespace Shop.Data.DB.Repositories
{
    class ImageRepository : BaseRepository<Guid, Image>, IImageRepository
    {
        public ImageRepository(ShopDbContext dbContext)
            : base(dbContext) { }
    }
}
