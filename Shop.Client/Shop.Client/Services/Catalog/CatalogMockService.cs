using Shop.Client.Models.Catalog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shop.Client.Services.Catalog
{
    class CatalogMockService : ICatalogService
    {
        private ObservableCollection<Category> MockCategories = new ObservableCollection<Category>
        {
            new Category { Id = new Guid("19b85d01-ab5c-4a60-8bd1-93bd693375c2"), Title = "Category 1", Description = "Description a Category 1" },
            new Category { Id = new Guid("c6814ab4-e3d8-4054-a6d1-77080a689868"), Title = "Category 2", Description = "Description a Category 2" },
        };

        private ObservableCollection<Product> MockProducts = new ObservableCollection<Product>
        {
            new Product { Id = new Guid("425f5695-6015-41c5-a186-6ac77bdab197"), Title = "Product 1", Content = "Content 1", Price = (decimal)30.00, Status = "Status", CategoryId = new Guid("19b85d01-ab5c-4a60-8bd1-93bd693375c2"), ImageId = new Guid("36522f74-34cf-4913-9f82-225b44e9e879")  },
            new Product { Id = new Guid("a5aff2c4-5fa7-4ace-a162-2edc4fdf309a"), Title = "Product 2", Content = "Content 2", Price = (decimal)120.00, Status = "Status", CategoryId = new Guid("19b85d01-ab5c-4a60-8bd1-93bd693375c2"), ImageId = new Guid("44607422-cd38-4626-9c39-914b69b7a284") },
            new Product { Id = new Guid("ae60a680-ee86-49c7-80be-88e3f3110596"), Title = "Product 3", Content = "Content 3", Price = (decimal)250.30, Status = "Status", CategoryId = new Guid("c6814ab4-e3d8-4054-a6d1-77080a689868"), ImageId = new Guid("cd588599-abf8-468d-bf76-fca660980b79") },
        };

        public async Task<ObservableCollection<Category>> GetCategories()
        {
            await Task.Delay(10);

            return MockCategories;
        }

        public async Task<ObservableCollection<Product>> GetProduct()
        {
            await Task.Delay(10);

            return MockProducts;
        }
    }
}
