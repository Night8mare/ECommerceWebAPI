using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchEcommerce.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructure
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handling Functions
            #region Creating product
            public async Task<Product> CreateProductAsync(Product product)
            {
                await _context.Products.AddAsync(product);
                Log.Information($"Creating product :{product.Name}");
                await _context.SaveChangesAsync();
                Log.Information("Product created successfully");
                return product;
            }
            #endregion
            #region Delete product
            public async Task<int> DeleteProductAsync(int id)
            {
                Log.Information($"Deleting product ID: {id}");
                return await _context.Products.Where(p =>p.Id == id).ExecuteDeleteAsync();
            }
            #endregion
            #region Get all product list paginated
            public async Task<List<Product>> GetAllProductAsync(int pageNumber,int pageSize)
            {
                var skipNumber = (pageNumber - 1) * pageSize;
                Log.Information("Getting all product List");
                return await _context.Products.Skip(skipNumber).Take(pageSize).ToListAsync();
            }
            #endregion
            #region Get all product filtered paginated
            public async Task<List<Product>> GetAllProductFilterAsync(string? Name, decimal? PurchasePriceMin, decimal? PurchasePriceMax, string? OrderBy, int pageNumber, int pageSize)
            {
                var skipNumber = (pageNumber - 1) * pageSize;
                Log.Information($"Getting all product list by Filter");
                var query = _context.Products.Skip(skipNumber).Take(pageSize).AsQueryable();

                if (!string.IsNullOrEmpty(Name))
                {
                    Log.Information($"Product list filtered by Name:{Name}");
                    query = query.Where(q => q.Name == Name);
                }

                if (!string.IsNullOrEmpty(OrderBy) && OrderBy.ToLower() == "asc")
                {
                    Log.Information($"Product list filtered by OrderBy:{OrderBy}");
                    query = query.OrderBy(q => q.Name);
                }
                else if (!string.IsNullOrEmpty(OrderBy) && OrderBy.ToLower() == "desc")
                {
                    Log.Information($"Product list filtered by OrderBy:{OrderBy}");
                    query = query.OrderByDescending(q => q.Name);
                }

                if (PurchasePriceMin.HasValue)
                {
                    Log.Information($"Product list filtered by purchase price min:{PurchasePriceMin}");
                    query = query.Where(q => q.PurchasePrice >= PurchasePriceMin.Value);
                }

                if (PurchasePriceMax.HasValue)
                {
                    Log.Information($"Product list filtered by purchase price max:{PurchasePriceMax}");
                    query = query.Where(q => q.PurchasePrice <= PurchasePriceMax.Value);
                }
                Log.Information("Returning Product list after filtering");
                return await query.ToListAsync();
            }
            #endregion
            #region Get Product by ID
            public async Task<Product> GetProductByIdAsync(int id)
            {
                Log.Information($"Getting product ID: {id}");
                return await _context.Products.AsNoTracking()
                                              .FirstOrDefaultAsync(p => p.Id == id);
            }
            #endregion
            #region Updating product
            public async Task<int> UpdateProductAsync(int id, Product product)
            {
                Log.Information($"Updating product ID:{id}");
                return await _context.Products
                        .Where(p => p.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(p => p.Name, product.Name)
                            .SetProperty(o => o.Description, product.Description)
                            .SetProperty(o => o.PurchasePrice, product.PurchasePrice)
                            .SetProperty(o => o.StockQuantity, product.StockQuantity)
                            .SetProperty(o => o.IsAvailable, product.IsAvailable)
                        );
            }
            #endregion
        #endregion
    }
}
