using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.Domain.Contracts.Repositories;
using System.Threading.Tasks;

namespace Shop.Domain
{
    public interface IUnitOfWork
    {
        DbContext DbContext { get; }
        EntityEntry<T> GetEntry<T>(T entity) where T : class;
        //void EnableRequestLogging();
        //void DisableRequestLogging();
        IUserRepository Users { get; }
        IUserProfileRepository UserProfiles { get; }
        IRoleRepository Roles { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IImageRepository Images { get; }

        void DisableAutoDetectChanges();
        //void EnableLazyLoading();
        //void DisableLazyLoading();
        void Commit();
        Task CommitAsync();
        void Rollback();
        // Removes all tracked entities from current context
        void DetachAll();
    }
}
