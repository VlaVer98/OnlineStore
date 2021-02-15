using Shop.Client.Models.Catalog;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shop.Client.Services.Catalog
{
    public interface ICatalogService
    {
        Task<ObservableCollection<Product>> GetProduct();
        Task<ObservableCollection<Category>> GetCategories();
    }
}
