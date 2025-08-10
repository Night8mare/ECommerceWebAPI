using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Domain.RepositoryInterface.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductAsync(int pageNumber,int pageSize);
        Task<List<Product>> GetAllProductFilterAsync(string? Name, decimal? PurchasePriceMin, decimal? PurchasePriceMax, string? OrderBy, int pageNumber, int pageSize);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<int> UpdateProductAsync(int id, Product product);
        Task<int> DeleteProductAsync(int id);
    }
}
