using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories;
using Shop.Domain;
using Shop.Domain.Contracts.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _dbContext;

        private IUserRepository _userRepository;
        private IUserProfileRepository _userProfileRepository;
        private IRoleRepository _roleRepository;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IOrderRepository _orderRepository;
        private IImageRepository _imageRepository;
        private IOrderProductRepository _orderProductRepository;

        public UnitOfWork()
        {
            _dbContext = new ShopDbContext();
        }

        public DbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public EntityEntry<T> GetEntry<T>(T entity) where T : class
        {
            return _dbContext.Entry(entity);
        }

        public IUserRepository Users
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_dbContext)); }
        }
        public IUserProfileRepository UserProfiles
        {
            get { return _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_dbContext)); }
        }
        public IRoleRepository Roles
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext)); }
        }
        public IProductRepository Products
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_dbContext)); }
        }
        public ICategoryRepository Categories
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_dbContext)); }
        }
        public IOrderRepository Orders
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext)); }
        }
        public IImageRepository Images
        {
            get { return _imageRepository ?? (_imageRepository = new ImageRepository(_dbContext)); }
        }
        public IOrderProductRepository OrderProducts
        {
            get { return _orderProductRepository ?? (_orderProductRepository = new OrderProductRepository(_dbContext)); }
        }

        public void Commit()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            _dbContext.ChangeTracker.DetectChanges();
            await _dbContext.SaveChangesAsync();
        }

        public void DetachAll()
        {
            var entries = _dbContext.ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        public void DisableAutoDetectChanges()
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        /*public void DisableLazyLoading()
        {
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }

        public void EnableLazyLoading()
        {
            _dbContext.Configuration.LazyLoadingEnabled = true;
        }*/

        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
